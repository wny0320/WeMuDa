using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;
    public static InventoryManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InventoryManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    instance = obj.AddComponent<InventoryManager>();
                    obj.name = typeof(InventoryManager).Name;
                }
            }
            return instance;
        }
    }
    public Dictionary<Item, int> Items = new Dictionary<Item, int>();

    private bool isDataSet = false; // 데이터 세팅이 되어있는지

    public GameObject ItemBox;
    private Transform slotParent;
    private List<Slot> slots;

    private Item clickItem;
    private GameObject itemInfo;
    private Image itemInfoImage;
    private TMP_Text itemInfoName;
    private TMP_Text itemInfoDetail;
    Canvas canvas;
    GraphicRaycaster graphicRaycaster;
    PointerEventData pointerEvent;

    //Scene이 전환될 때 오브젝트들이 사라지므로 넣어둔 데이터들이 증발할 것임
    //따라서 데이터를 보존하거나 탐색으로 넣어줘야함
    //데이터를 보존하는 것은 Json파일로 추출해야하는 것인데, Transform과 같은 데이터는 추출이 불가능함
    //따라서 리소스를 많이 잡아먹더라도 탐색을 사용해야함
    /// <summary>
    /// 들어가야하는 데이터들을 탐색으로 집어넣는 함수
    /// </summary>
    public void InventoryDataSet()
    {
        if (GameSceneManager.Instance.NowSceneName != GameSceneManager.SceneName.CampScene)
        {
            isDataSet = false;
            return;
        }
        if (isDataSet == true) return;
        slotParent = GameObject.Find("ItemBox/Viewport").transform;
        slots = new List<Slot>(slotParent.GetComponentsInChildren<Slot>());

        itemInfo = GameObject.Find("ItemInfo");
        itemInfoImage = itemInfo.transform.Find("ItemImage").GetComponent<Image>();
        itemInfoName = itemInfo.transform.Find("ItemName").GetComponent<TMP_Text>();
        itemInfoDetail = itemInfo.transform.Find("ItemDetail").GetComponent<TMP_Text>();

        itemInfo.SetActive(false);
        ItemBox.SetActive(false);

        graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
        pointerEvent = new PointerEventData(null);
        isDataSet = true;
    }
    public void ItemDetail()
    {
        if (GameSceneManager.Instance.NowSceneName != GameSceneManager.SceneName.CampScene) return;
        if (Input.GetMouseButtonDown(0))
        {
            pointerEvent.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            graphicRaycaster.Raycast(pointerEvent, results);
            if (results.Count > 0 && results[0].gameObject.TryGetComponent(out Slot slot))
            {
                int i = 0;
                Item target = null;
                foreach(Item j in Items.Keys)
                {
                    if(i >= slots.IndexOf(results[0].gameObject.GetComponent<Slot>()))
                    {
                        target = j;
                        break;
                    }
                    i++;
                }
                if(target != null)
                {
                    clickItem = target;
                    itemInfo.SetActive(true);
                    itemInfo.transform.position = pointerEvent.position;
                    itemInfoName.text = clickItem.ItemName;
                    itemInfoImage.sprite = clickItem.ItemSprite;
                    itemInfoDetail.text = clickItem.ItemExplain;
                }
                else
                {
                    clickItem = null;
                    itemInfo.SetActive(false);
                }
            }
        }
        if(BaseCampUiManager.Instance.itemBox.activeSelf == false)
        {
            itemInfo.SetActive(false);
        }
    }

    public void FreshSlot()
    {
        if (GameSceneManager.Instance.NowSceneName != GameSceneManager.SceneName.CampScene) return;
        int i = 0;
        for (; i < Items.Keys.Count && i < slots.Count; i++)
        {
            int k = 0;
            Item target = null;
            foreach(Item j in Items.Keys)
            {
                if (k >= i)
                {
                    target = j;
                    break;
                }
                k++;
            }
            slots[i].item = target;
            slots[i].amountText.text = Items[target].ToString();
            slots[i].amountText.gameObject.SetActive(true);
        }
        for (; i < slots.Count; i++)
        {
            slots[i].item = null;
            slots[i].amountText.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// 아이템을 해당 개수만큼 추가해주는 코드
    /// </summary>
    /// <param name="_item">추가시킬 아이템</param>
    /// <param name="_amount">아이템의 수량, 미입력시 기본 1</param>
    public void AddItem(Item _item, int _amount = 1)
    {
        if(Items.ContainsKey(_item))
        {
            int i = 0;
            foreach(Item j in Items.Keys)
            {
                if(j == _item)
                    break;
                i++;
            }
            Items[_item] += _amount;
            FreshSlot();
        }
        else
        {
            if (Items.Keys.Count < slots.Count)
            {
                Items.Add(_item, _amount);
                int i = 0;
                foreach (Item j in Items.Keys)
                {
                    if (j == _item)
                        break;
                    i++;
                }
                FreshSlot();
            }
            else
                Debug.Log("슬롯이 가득참");
                //메세지 띄우기 
        }
    }
    /// <summary>
    /// 아이템을 해당 개수만큼 줄여주는 코드
    /// </summary>
    /// <param name="_item">줄일 아이템</param>
    /// <param name="_amount">줄일 수량, 미입력시 기본 1</param>
    public void Subtract(Item _item, int _amount = 1)
    {
        if (Items.ContainsKey(_item))
        {
            int i = 0;
            foreach (Item j in Items.Keys)
            {
                if (j == _item)
                    break;
                i++;
            }
            Items[_item] -= _amount;
            if (Items[_item] < 0) Items[_item] = 0; // 마이너스 수량이 되는 것 방지
            FreshSlot();
        }
        else
            Debug.Log("해당 아이템이 존재하지 않음");
            //메세지 띄우기
    }
    /// <summary>
    /// 아이템 자체를 소멸시키는 함수, 빼는 함수는 따로 구현
    /// </summary>
    /// <param name="_item">소멸시킬 아이템</param>
    public void RemoveItem(Item _item)
    {
        int i = 0;
        foreach (Item j in Items.Keys)
        {
            if(j == _item)
                break;
            i++;
        }
        Items.Remove(_item);
        FreshSlot();
    }
    private void Update()
    {
        InventoryDataSet();
        ItemDetail();
    }
}
