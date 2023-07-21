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
    /// 산소의 양을 관리하는 함수
    /// </summary>
    /// <param name="_target">산소의 양을 조절할 오브젝트</param>
    /// <param name="_amount">조절할 산소의 양(추가는 양수, 감소는 음수)</param>
    public void ManageOxygen(GameObject _target, float _amount)
    {
        //OxygenUnit일 경우만 실행
        if(_target.TryGetComponent<OxygenUnit>(out OxygenUnit oxygenUnit))
        {
            oxygenUnit.Oxygen += _amount;
        }
    }
}
