using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ManufactManager : MonoBehaviour
{
    enum materialKind // ������ �� ����� ������ ���� ���� ������
    {
        amount = 3, // �� 3���� ��ᰡ �ִ� �� �� ����
    }
    // �̱���
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
    // ������ ���� â�� UI GameObject
    public GameObject ManufactUi;
    // ���� ������ �����۵��� ����Ʈ
    public List<Item> ManufactItem = new List<Item>();
    [SerializeField] // ���� �������� �̹���
    private Image manufactItemImage;
    [SerializeField] // ���� �������� �� ������
    private GameObject manufactItemPrefab;
    [SerializeField] // ���� ������ �������� �θ� �� ViewPort�� Content �κ�
    private GameObject bluePrint;
    [SerializeField] // ��� �������� ���� ���� ������Ʈ�� ����Ʈ
    private List<GameObject> materialUi;
    private Item item = null;
    [SerializeField] // ��ᰡ �ִ� 3���̹Ƿ� ũ�Ⱑ 3�� ����Ʈ
    private List<Image> materialItemImage;
    [SerializeField] // ���� ��� �������� ���� ���� �ʿ��� ��� �������� ���� �ؽ�Ʈ�� ���� ����Ʈ
    private List<TMP_Text> materialAmount;
    // ���� ��� ������ ���� �ʿ��� ��� �������� ���� �ؽ�Ʈ�� �����ϴ� / �ؽ�Ʈ�� ���� ����Ʈ
    private List<TMP_Text> materialAmountSlash = new List<TMP_Text>();
    [SerializeField] // ���� ������ ���� ���� Ȯ���� ���� �ؽ�Ʈ
    private TMP_Text successRateValue;
    private void manufactSetting()
    {
        // ��� Ui�� ����ŭ ��� �̹��� ������Ʈ�� �ؽ�Ʈ ������Ʈ�� �޾ƿ��� �ڵ�
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
            // _bluePrint �θ� ������Ʈ�� ���� �̹����� �ϳ� �ֱ� ������ �ι�°
            Image _bluePrintImage = _bluePrint.transform.Find("ItemImage").GetComponent<Image>();
            TMP_Text _bluePrintText = _bluePrint.GetComponentInChildren<TMP_Text>();

            _bluePrintImage.sprite = ManufactItem[i].ItemSprite;
            // onClick ���� ���� �ൿ�� �ڵ�󿡼� �־���
            // ���ٽ����� ���
            int value = i;
            _bluePrintButton.onClick.AddListener(() =>
            {
                item = ManufactItem[value];
            });
            _bluePrintText.text = ManufactItem[i].ItemName;
        }
    }
    private void refreshManufactData()
    {
        //UI�� �����ִٸ� ������ ������ ���� ����
        if (ManufactUi.activeSelf == false) return;
        // �������� ���õ��� �ʾҴٸ� ù��° ���������� ���� ��
        if (item == null) item = ManufactItem[0];
        manufactItemImage.sprite = item.ItemSprite;
        // ���ǽĿ� �� �� �� �� �̻� �����ϱ� ������ ���� Count�� ���ִ� ���� ����
        // �߰��� �ݺ����� ���ư��� ���߿� �����Ͱ� �ٲ�� ���� �����̴�.
        int materialItemKeysCount = item.MaterialItems.Keys.Count();
        // �̱����� �� �� �̻� ���ÿ��� �ν��Ͻ�ȭ ��Ű�°� �� ���� �����ϴ� ���� ���� ���ҽ��� �Ʋ���
        InventoryManager inv = InventoryManager.Instance;
        for (int i = 0; i < materialItemKeysCount; i++)
        {
            // Dictionary�� ������ �� �ε��� ������� �����ϰ� �ʹٸ� System.Linq�� �ִ� ElemantAt�� ���
            materialItemImage[i].sprite = item.MaterialItems.Keys.ElementAt(i).ItemSprite;
            // ������ �ִ� �翡 ���� ��������
            // �ش� �������� ������ �ִٸ� �ش� ������ ǥ��
            if (inv.Items.ContainsKey(item.MaterialItems.Keys.ElementAt(i)))
                materialAmount[i * 2].text = inv.Items[item.MaterialItems.Keys.ElementAt(i)].ToString();
            // ���ٸ� 0���� ���
            else
                materialAmount[i * 2].text = "0";
            // �����ϴ� �� �ʿ��� �翡 ���� ��������
            materialAmount[i * 2 + 1].text = item.MaterialItems.Values.ElementAt(i).ToString();
            // Ű�� ������ŭ, �� ��� �������� ������ŭ Ui Ȱ��ȭ
            materialUiActive(true, i);
        }
        for (int i = materialItemKeysCount; i < (int)materialKind.amount; i++)
        {
            // Ű�� ����, �� ��� �������� ������ ���� Ui ��Ȱ��ȭ
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
        //���� ���� Ȯ���� ���� ���
    }
    private void Awake()
    {
        manufactSetting();
        ManufactUi.SetActive(false);
    }
    private void Update()
    {
        refreshManufactData();
    }
}