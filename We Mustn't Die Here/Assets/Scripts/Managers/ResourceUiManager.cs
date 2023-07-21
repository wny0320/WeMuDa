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
    [SerializeField] TMP_Text FoodText;
    [SerializeField] TMP_Text WaterText;
    [SerializeField] TMP_Text PersonText;
    [SerializeField] TMP_Text TurnText;

    public int FOOD;
    public int WATER;
    public int PERSON;
    public int TURN;


    private void Update()
    {
        RsSync();
    }
    void RsSync()
    {
        PERSON = MinerCount();

        FoodText.text = FOOD.ToString();
        WaterText.text = WATER.ToString();
        PersonText.text = PERSON.ToString();
        TurnText.text = TURN.ToString();
    }
    int MinerCount()
    {
        int num = 0;
        foreach(Miner i in MinerManager.Instance.MinerList)
        {
            if (i.gameObject.activeSelf == true)
                num++;
        }
        return num;
    }
}
