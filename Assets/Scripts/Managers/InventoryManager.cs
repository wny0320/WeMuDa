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

    private bool isDataSet = false; // ������ ������ �Ǿ��ִ���

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

    //Scene�� ��ȯ�� �� ������Ʈ���� ������Ƿ� �־�� �����͵��� ������ ����
    //���� �����͸� �����ϰų� Ž������ �־������
    //�����͸� �����ϴ� ���� Json���Ϸ� �����ؾ��ϴ� ���ε�, Transform�� ���� �����ʹ� ������ �Ұ�����
    //���� ���ҽ��� ���� ��ƸԴ��� Ž���� ����ؾ���
    /// <summary>
    /// �����ϴ� �����͵��� Ž������ ����ִ� �Լ�
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
    /// �������� �ش� ������ŭ �߰����ִ� �ڵ�
    /// </summary>
    /// <param name="_item">�߰���ų ������</param>
    /// <param name="_amount">�������� ����, ���Է½� �⺻ 1</param>
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
                Debug.Log("������ ������");
                //�޼��� ���� 
        }
    }
    /// <summary>
    /// �������� �ش� ������ŭ �ٿ��ִ� �ڵ�
    /// </summary>
    /// <param name="_item">���� ������</param>
    /// <param name="_amount">���� ����, ���Է½� �⺻ 1</param>
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
            if (Items[_item] < 0) Items[_item] = 0; // ���̳ʽ� ������ �Ǵ� �� ����
            FreshSlot();
        }
        else
            Debug.Log("�ش� �������� �������� ����");
            //�޼��� ����
    }
    /// <summary>
    /// ������ ��ü�� �Ҹ��Ű�� �Լ�, ���� �Լ��� ���� ����
    /// </summary>
    /// <param name="_item">�Ҹ��ų ������</param>
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
