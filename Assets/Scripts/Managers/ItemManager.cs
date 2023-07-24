using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 모든 아이템 속성을 가지고 있는 ItemList로 아이템을 찾기 쉽게 하는 것이 목적인 스크립트
/// </summary>
public class ItemManager : MonoBehaviour
{
    public enum ItemName
    {
        //아이템 이름을 한글로 적고 해당 인덱스를 적어서 순서대로 정렬할 예정
    }
    static ItemManager instance;
    public static ItemManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ItemManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    instance = obj.AddComponent<ItemManager>();
                    obj.name = typeof(ItemManager).Name;
                }
            }
            return instance;
        }
    }
    public List<Item> ItemList; // 모든 아이템 리스트
}