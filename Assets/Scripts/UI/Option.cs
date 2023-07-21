using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    [SerializeField] GameObject OptionUI;
    [SerializeField] bool IsOptionOp;

    private void Awake()
    {
        CloseOption();
    }
    public void OptionMenu()
    {
        if (!IsOptionOp)
            OpenOption();
        else
            CloseOption();
    }
    void OpenOption()
    {
        OptionUI.SetActive(true);
        IsOptionOp = true;
    }

    public void CloseOption()
    {
        OptionUI.SetActive(false);
        IsOptionOp = false;
    }
}
