using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCampUiManager : MonoBehaviour
{
    static BaseCampUiManager instance;
    public static BaseCampUiManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BaseCampUiManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    instance = obj.AddComponent<BaseCampUiManager>();
                    obj.name = typeof(BaseCampUiManager).Name;
                }
            }
            return instance;
        }
    }
    [SerializeField] List<GameObject> btList;
    [SerializeField] bool isMenuOpen;
    public GameObject itemBox;
    List<GameObject> uiList = new List<GameObject>();
    private bool isUiOpen = false;

    private void uiListSet()
    {
        uiList.Add(itemBox);
        uiList.Add(ManufactManager.Instance.ManufactUi);
        uiList.Add(MinerInfoManager.Instance.ExploreTeamOrganizeUi);
        //uiList.Add(fishing);
        //uiList.Add(Minning);
    }
    public void MenuUiManage()
    {
        if (isMenuOpen)
            closeMenu();
        else
            openMenu();
    }
    private void openMenu()
    {
        foreach (var i in btList)
        {
            i.SetActive(true);
            isMenuOpen = true;
        }
    }
    private void closeMenu()
    {
        foreach (var i in btList)
        {
            i.SetActive(false);
            isMenuOpen = false;
        }
    }

    public void ItemBoxUiManage()
    {
        if (itemBox.activeSelf)
        {
            itemBox.SetActive(false);
            isUiOpen = false;
        }
        
        else
        {
            closeOpenedUi();
            itemBox.SetActive(true);
            isUiOpen = true;
        }
    }
    public void ManufactUiManage()
    {
        if(ManufactManager.Instance.ManufactUi.activeSelf)
        {
            ManufactManager.Instance.ManufactUi.SetActive(false);
            isUiOpen = false;
        }
        else
        {
            closeOpenedUi();
            ManufactManager.Instance.ManufactUi.SetActive(true);
            isUiOpen = true;
        }
    }
    public void ExploreTeamOrganizeUiManage()
    {
        if (MinerInfoManager.Instance.ExploreTeamOrganizeUi.activeSelf)
        {
            MinerInfoManager.Instance.ExploreTeamOrganizeUi.SetActive(false);
            isUiOpen = false;
        }
        else
        {
            closeOpenedUi();
            MinerInfoManager.Instance.ExploreTeamOrganizeUi.SetActive(true);
            isUiOpen = true;
        }
    }
    private void closeOpenedUi()
    {
        if(isUiOpen)
        {
            for(int i = 0; i < uiList.Count; i++)
            {
                uiList[i].SetActive(false);
            }
            isUiOpen = false;
        }
    }
    public void DayStartButton()
    {

    }
    private void Awake()
    {
        closeMenu();
        uiListSet();
        itemBox.SetActive(false);
    }
}
