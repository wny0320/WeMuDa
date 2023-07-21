using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private static ResourceManager instance;
    public static ResourceManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ResourceManager>().GetComponent<ResourceManager>();
                if (instance = null)
                {
                    GameObject gameObject = new GameObject();
                    instance = gameObject.AddComponent<ResourceManager>();
                    gameObject.name = typeof(ResourceManager).ToString();
                }
            }
            return instance;
        }
    }
    //resource[0] = 식량, resource[1] = 식수
    private List<int> resource = new List<int>();
    [SerializeField]
    private int m_initFoodAmount;
    [SerializeField]
    private int m_initWaterAmount;

    /// <summary>
    /// 외부에 resource 데이터를 반환시켜주는 함수, UI 작성을 위한 함수
    /// </summary>
    /// <returns>resource[0] = 식량, resource[1] = 식수</returns>
    public List<int> GetResource()
    {
        return resource;
    }

    /// <summary>
    /// resource 초기화 함수
    /// </summary>
    public void InitResource()
    {
        resource = new List<int>();
        resource.Add(m_initFoodAmount);
        resource.Add(m_initWaterAmount);
    }
    /// <summary>
    /// 자원을 증감시키는 함수, 증가는 양수 감소는 음수
    /// </summary>
    /// <param name="_foodAmount">증감할 식량의 양</param>
    /// <param name="_waterAmount">증감할 식수의 양</param>
    public void ManagerResource(int _foodAmount, int _waterAmount)
    {
        resource[0] += _foodAmount;
        resource[1] += _waterAmount;
    }
    
    private void Awake()
    {
        InitResource();
    }
}
