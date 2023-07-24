using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceUiManager : MonoBehaviour
{
    private static ResourceUiManager instance;
    public static ResourceUiManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<ResourceUiManager>();
                if(instance == null)
                {
                    GameObject obj = new GameObject();
                    instance = obj.AddComponent<ResourceUiManager>();
                    obj.name = typeof(ResourceUiManager).Name;
                }
            }
            return instance;
        }
    }
    TMP_Text FoodText;
    TMP_Text PersonText;
    TMP_Text WaterText;
    TMP_Text TurnText;

    public int Food;
    public int Water;
    public int Person;
    public int Turn;

    private bool isUiDataSet = false;

    private void rsUiDataSet()
    {
        if (GameSceneManager.Instance.NowSceneName != GameSceneManager.SceneName.CampScene)
        {
            isUiDataSet = false;
            return;
        }
        if (isUiDataSet == true) return;
        Transform m_resourceUi = GameObject.Find("ResourceUI").transform;
        FoodText = m_resourceUi.transform.Find("FoodText").GetComponent<TMP_Text>();
        PersonText = m_resourceUi.transform.Find("PersonText").GetComponent<TMP_Text>();
        WaterText = m_resourceUi.transform.Find("WaterText").GetComponent<TMP_Text>();
        TurnText = m_resourceUi.transform.Find("TurnText").GetComponent<TMP_Text>();
        isUiDataSet = true;
    }

    private void RsSync()
    {
        if (GameSceneManager.Instance.NowSceneName != GameSceneManager.SceneName.CampScene) return;
        Person = MinerCount();

        FoodText.text = Food.ToString();
        WaterText.text = Water.ToString();
        PersonText.text = Person.ToString();
        TurnText.text = Turn.ToString();
    }
    private int MinerCount()
    {
        int num = 0;
        foreach(Miner i in MinerManager.Instance.MinerList)
        {
            if (i.gameObject.activeSelf == true)
                num++;
        }
        return num;
    }
    private void Update()
    {
        rsUiDataSet();
        RsSync();
    }
}
