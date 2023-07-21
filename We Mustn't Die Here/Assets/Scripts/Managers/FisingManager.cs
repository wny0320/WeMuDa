using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FisingManager : MonoBehaviour
{
    private static FisingManager instance;
    public static FisingManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<FisingManager>().GetComponent<FisingManager>();
                if (instance = null)
                {
                    GameObject gameObject = new GameObject();
                    instance = gameObject.AddComponent<FisingManager>();
                    gameObject.name = typeof(FisingManager).ToString();
                }
            }
            return instance;
        }
    }
    private bool canFishing = false;
    public bool isFising = false;
    [SerializeField]
    private Button fishingBt;

    private void enableFishing()
    {
        canFishing = true;
        //���� ��ư Ȱ��ȭ
        fishingBt.interactable = true;
    }
    private void disableFishing()
    {
        canFishing = false;
        //���� ��ư ��Ȱ��ȭ
        fishingBt.interactable = false;
    }

    public void ManageFishing()
    {
        if(isFising == true)//�������� ExploreManager���� �ʵ����Ϳ� ���� �ִ��� üũ�ؾ���
        {
            disableFishing();
        }
        else
        {
            enableFishing();
        }
    }

    public void Fishing()
    {
        if(canFishing == true)
        {
            //������ Ư���� ���� ���ø� ������Ŵ
            //������ ����� = �ķ�
        }
    }
}
