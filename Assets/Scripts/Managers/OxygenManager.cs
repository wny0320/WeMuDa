using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenManager : MonoBehaviour
{
    public static OxygenManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<OxygenManager>().GetComponent<OxygenManager>();
                if(instance == null)
                {
                    GameObject gameObject = new GameObject();
                    instance = gameObject.AddComponent<OxygenManager>();
                    gameObject.name = typeof(OxygenManager).ToString();
                }
            }
            return instance;
        }
    }
    private static OxygenManager instance;
    /// <summary>
    /// ����� ���� �����ϴ� �Լ�
    /// </summary>
    /// <param name="_target">����� ���� ������ ������Ʈ</param>
    /// <param name="_amount">������ ����� ��(�߰��� ���, ���Ҵ� ����)</param>
    public void ManageOxygen(GameObject _target, float _amount)
    {
        //OxygenUnit�� ��츸 ����
        if(_target.TryGetComponent<OxygenUnit>(out OxygenUnit oxygenUnit))
        {
            oxygenUnit.Oxygen += _amount;
        }
    }
}
