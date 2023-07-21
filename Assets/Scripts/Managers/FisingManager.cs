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
        //낚시 버튼 활성화
        fishingBt.interactable = true;
    }
    private void disableFishing()
    {
        canFishing = false;
        //낚시 버튼 비활성화
        fishingBt.interactable = false;
    }

    public void ManageFishing()
    {
        if(isFising == true)//조건으로 ExploreManager에서 맵데이터에 강이 있는지 체크해야함
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
            //광부의 특성에 따라 낚시를 성공시킴
            //낚시한 물고기 = 식량
        }
    }
}
