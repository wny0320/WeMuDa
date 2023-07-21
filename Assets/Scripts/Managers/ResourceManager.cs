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
    //resource[0] = �ķ�, resource[1] = �ļ�
    private List<int> resource = new List<int>();
    [SerializeField]
    private int m_initFoodAmount;
    [SerializeField]
    private int m_initWaterAmount;

    /// <summary>
    /// �ܺο� resource �����͸� ��ȯ�����ִ� �Լ�, UI �ۼ��� ���� �Լ�
    /// </summary>
    /// <returns>resource[0] = �ķ�, resource[1] = �ļ�</returns>
    public List<int> GetResource()
    {
        return resource;
    }

    /// <summary>
    /// resource �ʱ�ȭ �Լ�
    /// </summary>
    public void InitResource()
    {
        resource = new List<int>();
        resource.Add(m_initFoodAmount);
        resource.Add(m_initWaterAmount);
    }
    /// <summary>
    /// �ڿ��� ������Ű�� �Լ�, ������ ��� ���Ҵ� ����
    /// </summary>
    /// <param name="_foodAmount">������ �ķ��� ��</param>
    /// <param name="_waterAmount">������ �ļ��� ��</param>
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
