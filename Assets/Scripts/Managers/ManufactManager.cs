using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ManufactManager : MonoBehaviour
{
    enum materialKind // 제조할 때 재료의 종류의 수를 담은 열거형
    {
        amount = 3, // 총 3개의 재료가 최대 들어갈 수 있음
    }
    // 싱글톤
    private static ManufactManager instance;
    public static ManufactManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ManufactManager>();
                if (instance == null)
                {
                    GameObject gameObject = new GameObject();
                    instance = gameObject.AddComponent<ManufactManager>();
                    gameObject.name = typeof(ManufactManager).ToString();
                }
            }
            return instance;
        }
    }
    // 아이템 제작 창의 UI GameObject
    public GameObject ManufactUi;
    // 제조 가능한 아이템들의 리스트
    public List<Item> ManufactItem = new List<Item>();
    // 제조 아이템의 이미지
    private Image manufactItemImage;
    // 제조 아이템이 들어갈 프리팹
    [SerializeField]
    private GameObject manufactItemPrefab;
    // 제조 아이템 프리팹의 부모가 될 ViewPort의 Content 부분
    private GameObject bluePrint;
    // 재료 아이템이 들어가는 게임 오브젝트의 리스트
    private List<GameObject> materialUi;
    private Item item = null;
    // 재료가 최대 3개이므로 크기가 3인 리스트
    private List<Image> materialItemImage;
    // 현재 재료 아이템을 가진 수와 필요한 재료 아이템의 수의 텍스트를 지닌 리스트
    private List<TMP_Text> materialAmount;
    // 현재 재료 아이템 수와 필요한 재료 아이템의 수의 텍스트를 구별하는 / 텍스트를 담은 리스트
    private List<TMP_Text> materialAmountSlash = new List<TMP_Text>();
    // 현재 아이템 제작 성공 확률에 대한 텍스트
    private TMP_Text successRateValue;

    private bool isUiDataSet = false;
    private void manufactSetting()
    {
        // 현재씬이 캠프씬이 아닐경우 return
        if (GameSceneManager.Instance.NowSceneName != GameSceneManager.SceneName.CampScene)
        {
            isUiDataSet = false;
            return;
        }
        if (isUiDataSet == true) return;
        // 다시 컴포넌트 찾기
        isUiDataSet = true;
        ManufactUi = GameObject.Find("ManufactUI");
        bluePrint = ManufactUi.transform.Find("ItemList").transform.Find("Viewport").Find("Content").gameObject;
        successRateValue = ManufactUi.transform.Find("BlueprintList").Find("SuccessRate").Find("SuccessRateValue").GetComponent<TMP_Text>();

        materialUi = new List<GameObject>();
        materialItemImage = new List<Image>();
        materialAmount = new List<TMP_Text>();

        Transform m_bluePrintList = ManufactUi.transform.Find("BlueprintList");
        materialUi.Add(m_bluePrintList.Find("MaterialItem1").gameObject);
        materialUi.Add(m_bluePrintList.Find("MaterialItem2").gameObject);
        materialUi.Add(m_bluePrintList.Find("MaterialItem3").gameObject);

        manufactItemImage = m_bluePrintList.Find("ItemImage").GetComponent<Image>();

        int m_materialItemCount = (int)materialKind.amount;
        for(int i = 0; i < m_materialItemCount; i++)
        {
            materialItemImage.Add(null);
        }

        // 재료 Ui의 수만큼 재료 이미지 컴포넌트와 텍스트 컴포넌트를 받아오는 코드
        int materialUiCount = materialUi.Count;
        for (int i = 0; i < materialUiCount; i++)
        {
            materialItemImage[i] = materialUi[i].GetComponentInChildren<Image>();
            TMP_Text[] textBuffer = materialUi[i].GetComponentsInChildren<TMP_Text>();
            materialAmount.Add(textBuffer[0]);
            materialAmount.Add(textBuffer[2]);
            materialAmountSlash.Add(textBuffer[1]);
        }
        int manufactItemCount = ManufactItem.Count;
        for (int i = 0; i < manufactItemCount; i++)
        {
            GameObject _bluePrint = Instantiate(manufactItemPrefab);
            _bluePrint.transform.SetParent(bluePrint.transform);
            Button _bluePrintButton = _bluePrint.GetComponent<Button>();
            // _bluePrint 부모 오브젝트에 투명 이미지가 하나 있기 때문에 두번째
            Image _bluePrintImage = _bluePrint.transform.Find("ItemImage").GetComponent<Image>();
            TMP_Text _bluePrintText = _bluePrint.GetComponentInChildren<TMP_Text>();

            _bluePrintImage.sprite = ManufactItem[i].ItemSprite;
            // onClick 했을 때의 행동을 코드상에서 넣어줌
            // 람다식으로 사용
            int value = i;
            _bluePrintButton.onClick.AddListener(() =>
            {
                item = ManufactItem[value];
            });
            _bluePrintText.text = ManufactItem[i].ItemName;
        }
        ManufactUi.SetActive(false);
    }
    private void refreshManufactData()
    {
        if (GameSceneManager.Instance.NowSceneName != GameSceneManager.SceneName.CampScene) return;
        //UI가 꺼져있다면 데이터 갱신을 하지 않음
        if (ManufactUi == null) return;
        if (ManufactUi.activeSelf == false) return;
        // 아이템이 선택되지 않았다면 첫번째 아이템으로 띄우게 됨
        if (item == null) item = ManufactItem[0];
        manufactItemImage.sprite = item.ItemSprite;
        // 조건식에 들어갈 때 두 번 이상씩 참조하기 때문에 따로 Count를 빼주는 것이 좋음
        // 추가로 반복문이 돌아가는 도중에 데이터가 바뀌는 경우는 예외이다.
        int materialItemKeysCount = item.MaterialItems.Keys.Count();
        // 싱글톤을 두 번 이상 사용시에는 인스턴스화 시키는게 두 번씩 참조하는 것을 막아 리소스를 아껴줌
        InventoryManager inv = InventoryManager.Instance;
        for (int i = 0; i < materialItemKeysCount; i++)
        {
            // Dictionary를 접근할 때 인덱스 기반으로 접근하고 싶다면 System.Linq에 있는 ElemantAt을 사용
            materialItemImage[i].sprite = item.MaterialItems.Keys.ElementAt(i).ItemSprite;
            // 가지고 있는 양에 대한 리프레시
            // 해당 아이템을 가지고 있다면 해당 수량을 표시
            if (inv.Items.ContainsKey(item.MaterialItems.Keys.ElementAt(i)))
                materialAmount[i * 2].text = inv.Items[item.MaterialItems.Keys.ElementAt(i)].ToString();
            // 없다면 0으로 기록
            else
                materialAmount[i * 2].text = "0";
            // 제작하는 데 필요한 양에 대한 리프레시
            materialAmount[i * 2 + 1].text = item.MaterialItems.Values.ElementAt(i).ToString();
            // 키의 갯수만큼, 즉 재료 아이템의 갯수만큼 Ui 활성화
            materialUiActive(true, i);
        }
        for (int i = materialItemKeysCount; i < (int)materialKind.amount; i++)
        {
            // 키의 갯수, 즉 재료 아이템의 갯수를 넘은 Ui 비활성화
            materialUiActive(false, i);
        }
    }

    private void materialUiActive(bool _tf, int _index)
    {
        materialItemImage[_index].gameObject.SetActive(_tf);
        materialAmount[_index * 2].gameObject.SetActive(_tf);
        materialAmount[_index * 2 + 1].gameObject.SetActive(_tf);
        materialAmountSlash[_index].gameObject.SetActive(_tf);
    }
    private void successRateCaculate()
    {
        //추후 성공 확률에 대한 계산
    }
    private void Update()
    {
        manufactSetting();
        refreshManufactData();
    }
}
