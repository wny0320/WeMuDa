using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerDontDestroy : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
