using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBt : MonoBehaviour
{
    [SerializeField] Item i1;
    [SerializeField] Item i2;
    [SerializeField] Item i3;
    public void AddI1()
    {
        InventoryManager.Instance.AddItem(i1, 1);
    }
    public void DelI1()
    {
        InventoryManager.Instance.RemoveItem(i1);
    }
    public void AddI2()
    {
        InventoryManager.Instance.AddItem(i2, 1);
    }
    public void DelI2()
    {
        InventoryManager.Instance.RemoveItem(i2);
    }
    public void AddI3()
    {
        InventoryManager.Instance.AddItem(i3, 1);
    }
    public void DelI3()
    {
        InventoryManager.Instance.RemoveItem(i3);
    }
}
