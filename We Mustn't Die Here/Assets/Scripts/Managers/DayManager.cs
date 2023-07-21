using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    // ΩÃ±€≈Ê
    private static DayManager instance;
    public static DayManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<DayManager>();
                if(instance == null)
                {
                    GameObject gameObject = new GameObject();
                    instance = gameObject.AddComponent<DayManager>();
                    gameObject.name = typeof(DayManager).ToString();
                }
            }
            return instance;
        }
    }

    public int ActionPoint;
    private void dayStart()
    {
        ActionPoint = MinerManager.Instance.MinerList.Count;
    }
    public void DayTime()
    {
        // ¡¶¡∂, ≈Ω«Ë, ≥¨Ω√, √§±§ Ω««‡
        dayStart();

    }
}
