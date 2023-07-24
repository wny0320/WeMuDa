using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ��� ������ �Ӽ��� ������ �ִ� ItemList�� �������� ã�� ���� �ϴ� ���� ������ ��ũ��Ʈ
/// </summary>
public class ItemManager : MonoBehaviour
{
    public enum ItemName
    {
        //������ �̸��� �ѱ۷� ���� �ش� �ε����� ��� ������� ������ ����
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
    public List<Item> ItemList; // ��� ������ ����Ʈ
}