using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField]
    Image backgroundImage;
    private static BackgroundManager instance;
    public static BackgroundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BackgroundManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    instance = obj.AddComponent<BackgroundManager>();
                    obj.name = typeof(BackgroundManager).Name;
                }
            }
            return instance;
        }
    }
    public void SetBackground(Sprite _sprite)
    {
        backgroundImage.sprite = _sprite;
    }
}
