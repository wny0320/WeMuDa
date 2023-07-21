using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 모든 아이템 속성을 가지고 있는 ItemList로 드랍되거나 얻은 아이템을 추가해주는 스크립트
/// 사용법 InventoryMng.ins.AddItem(ItemList[0]);
/// </summary>
public class ItemManager : MonoBehaviour
{
    [SerializeField] List<Item> ItemList;
    private void Start()
    {
        InventoryManager.Instance.AddItem(ItemList[0], 1);
    }
}