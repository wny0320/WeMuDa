using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ��� ������ �Ӽ��� ������ �ִ� ItemList�� ����ǰų� ���� �������� �߰����ִ� ��ũ��Ʈ
/// ���� InventoryMng.ins.AddItem(ItemList[0]);
/// </summary>
public class ItemManager : MonoBehaviour
{
    [SerializeField] List<Item> ItemList;
    private void Start()
    {
        InventoryManager.Instance.AddItem(ItemList[0], 1);
    }
}