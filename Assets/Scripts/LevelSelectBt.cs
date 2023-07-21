using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectBt
{
    [SerializeField]
    private GameManager.LevelEnum m_level;

    public void LevelSelect()
    {
        GameManager.Instance.Level = m_level;
    }
}
