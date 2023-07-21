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

    [SerializeField]
    private Transform slotParent;
    [SerializeField]
    private List<Slot> slots;

    [SerializeField]
    private Item clickItem;
    [SerializeField]
    private GameObject itemInfo;
    [SerializeField]
    private TMP_Text itemInfoName;
    [SerializeField]
    private Image itemInfoImage;
    [SerializeField]
    private TMP_Text itemInfoDetail;
    [SerializeField]
    Canvas canvas;
    GraphicRaycaster graphicRaycaster;
    PointerEventData pointerEvent;

    void Awake()
    {
        FreshSlot();
        itemInfo.SetActive(false);
        graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
        pointerEvent = new PointerEventData(null);
    }
    private void Update()
    {
        ItemDetail();
    }
    public void ItemDetail()
    {
        if(Input.GetMouseButtonDown(0))
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

    public void AddItem(Item _item, int amount)
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
            Items[_item] += amount;
            FreshSlot();
        }
        else
        {
            if (Items.Keys.Count < slots.Count)
            {
                Items.Add(_item, amount);
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
                Debug.Log("½½·ÔÀÌ °¡µæÂü");
        }
    }

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
}
