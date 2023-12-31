using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera cam;
    static CameraManager instance;
    public static CameraManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CameraManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    instance = obj.AddComponent<CameraManager>();
                    obj.name = typeof(CameraManager).Name;
                }
            }
            return instance;
        }
    }
    public void CameraSet()
    {
        if(cam == null)
        cam = Camera.main;
    }

    private void Update()
    {
        CameraSet();
    }
}
