using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private GameObject itemBox;
    private List<GameObject> btList;
    private bool isMenuOpen;
    private List<GameObject> uiList = new List<GameObject>();
    private bool isUiOpen = false;

    private bool isDataSet = false;

    private bool uiListSet()
    {
        if (GameSceneManager.Instance.NowSceneName != GameSceneManager.SceneName.CampScene)
        {
            isDataSet = false;
            return false;
        }
        if (isDataSet == true) return true; // ������ ���� �Ϸ�� ���
        GameObject targetUiObject = InventoryManager.Instance.ItemBox;
        itemBox = targetUiObject;
        List<GameObject> m_ObjectList = new List<GameObject>();
        if (targetUiObject == null) return false;
        m_ObjectList.Add(targetUiObject);
        targetUiObject = ManufactManager.Instance.ManufactUi;
        if(targetUiObject == null) return false;
        m_ObjectList.Add(targetUiObject);
        targetUiObject = MinerInfoManager.Instance.ExploreTeamOrganizeUi;
        if (targetUiObject == null) return false;
        m_ObjectList.Add(targetUiObject);
        //uiList.Add(fishing);
        //uiList.Add(Minning);

        uiList = m_ObjectList.ToList(); // ���� ����
        return true;
    }
    private bool btListSet()
    {
        if (GameSceneManager.Instance.NowSceneName != GameSceneManager.SceneName.CampScene)
        {
            isDataSet = false;
            return false;
        }
        if (isDataSet == true) return true; // ������ ���� �Ϸ�� ���
        GameObject m_menuBar = GameObject.Find("MenuBarBt");
        List<GameObject> m_ObjectList = new List<GameObject>();
        m_ObjectList.Add(m_menuBar);
        m_ObjectList.Add(m_menuBar.transform.Find("Item").gameObject);
        m_ObjectList.Add(m_menuBar.transform.Find("Manufact").gameObject);
        m_ObjectList.Add(m_menuBar.transform.Find("Explore").gameObject);
        m_ObjectList.Add(m_menuBar.transform.Find("Fishing").gameObject);
        m_ObjectList.Add(m_menuBar.transform.Find("Minning").gameObject);
        if (m_ObjectList.Contains(null)) return false; // �ش� ��ư�� ���� �������� ���� ���(���� �ε�� ���)
        else
        {
            return true;
        }
    }
    private void listSet()
    {
        bool m_isUiListSet = uiListSet();
        bool m_isBtListSet = btListSet();
        if (m_isUiListSet & m_isBtListSet) isDataSet = true;
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
        GameObject m_manufactUi = ManufactManager.Instance.ManufactUi;
        if (m_manufactUi.activeSelf)
        {
            m_manufactUi.SetActive(false);
            isUiOpen = false;
        }
        else
        {
            closeOpenedUi();
            m_manufactUi.SetActive(true);
            isUiOpen = true;
        }
    }
    public void ExploreTeamOrganizeUiManage()
    {
        GameObject m_exploreTeamOrganizeUi = MinerInfoManager.Instance.ExploreTeamOrganizeUi;
        if (m_exploreTeamOrganizeUi.activeSelf)
        {
            m_exploreTeamOrganizeUi.SetActive(false);
            isUiOpen = false;
        }
        else
        {
            closeOpenedUi();
            m_exploreTeamOrganizeUi.SetActive(true);
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
        listSet();
        closeMenu();
        // �� ģ���� �������� �κ��丮 �Ŵ����� itemBox�� ã�� ����,
        // ���� �� �ڵ带 inventoryManager�� �ڵ带 �־����, �� ������ isDataSet ������ �̿��ϸ� �ɵ�?
    }
}
