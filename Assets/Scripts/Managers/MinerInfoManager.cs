using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System.Linq;
using System;

public class MinerInfoManager : MonoBehaviour
{
    private enum statName
    {
        Str,
        Agi,
        Vit,
        Dex,
        Int,
    }
    // 싱글톤
    private static MinerInfoManager instance;
    public static MinerInfoManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MinerInfoManager>();
                if (instance == null)
                {
                    GameObject gameObject = new GameObject();
                    instance = gameObject.AddComponent<MinerInfoManager>();
                    gameObject.name = typeof(MinerInfoManager).ToString();
                }
            }
            return instance;
        }
    }
    public GameObject ExploreTeamOrganizeUi;
    private bool isDataSet = false;

    private TMP_Text minerNameText;
    private List<TMP_Text> statTextList = new List<TMP_Text>();
    private List<TMP_Text> healthTextList = new List<TMP_Text>();
    private List<TMP_Text> perAbilityTextList = new List<TMP_Text>();
    private List<TMP_Text> posAbilityTextList = new List<TMP_Text>();
    private List<TMP_Text> negAbilityTextList = new List<TMP_Text>();

    public List<Miner> ExploreMinerList = new List<Miner>();
    public Miner TargetMiner = null;
    private int index = 0;

    private Image minerImage;
    private void MinerInfoSetting()
    {
        if (GameSceneManager.Instance.NowSceneName != GameSceneManager.SceneName.CampScene)
        {
            isDataSet = false;
            return;
        }
        if (isDataSet == true) return;
        ExploreTeamOrganizeUi = GameObject.Find("ExploreTeamOrganizeBackground");

        // 텍스트들 찾기
        Transform m_uiTransform = ExploreTeamOrganizeUi.transform;
        minerNameText = m_uiTransform.Find("MinerName").GetComponent<TMP_Text>();

        // 이미지 찾기
        
        // Enum으로 foreach 돌리기
        Array m_array = Enum.GetValues(typeof(statName));
        foreach (statName i in m_array)
        {
            statTextList.Add(m_uiTransform.Find(i.ToString() + "Value").GetComponent<TMP_Text>());
        }
        //statTextList.Add(m_uiTransform.Find("StrValue").GetComponent<TMP_Text>());
        //statTextList.Add(m_uiTransform.Find("AgiValue").GetComponent<TMP_Text>());
        //statTextList.Add(m_uiTransform.Find("VitValue").GetComponent<TMP_Text>());
        //statTextList.Add(m_uiTransform.Find("DexValue").GetComponent<TMP_Text>());
        //statTextList.Add(m_uiTransform.Find("IntValue").GetComponent<TMP_Text>());

        healthTextList.Add(m_uiTransform.Find("StressValue").GetComponent<TMP_Text>());
        healthTextList.Add(m_uiTransform.Find("HungryValue").GetComponent<TMP_Text>());
        healthTextList.Add(m_uiTransform.Find("ThirstyValue").GetComponent<TMP_Text>());

        int m_maxAbilityAmount = Ability.MaxAbilityAmount;
        int m_abilityKindAmount = Ability.abilityKindAmount;

        for(int i = 0; i < m_abilityKindAmount; i++)
        {
            List<TMP_Text> m_targetAbilityText = new List<TMP_Text>();
            string m_searchName = "";
            switch (i)
            {
                case 0:
                    m_targetAbilityText = perAbilityTextList;
                    m_searchName = "PerfectAbilityBackground/PerfectAbility";
                    break;
                case 1:
                    m_targetAbilityText = posAbilityTextList;
                    m_searchName = "PositiveAbilityBackground/PositiveAbility";
                    break;
                case 2:
                    m_targetAbilityText = negAbilityTextList;
                    m_searchName = "NegativeAbilityBackground/NegativeAbility";
                    break;
            }
            for (int j = 0; j < m_maxAbilityAmount; j++)
            {
                m_targetAbilityText.Add(m_uiTransform.Find(m_searchName + (j + 1).ToString()).GetComponent<TMP_Text>());
            }
        }

        ExploreTeamOrganizeUi.SetActive(false);
        isDataSet = true;
    }
    public void ShowMinerInfo()
    {
        if (GameSceneManager.Instance.NowSceneName != GameSceneManager.SceneName.CampScene) return;
        if (ExploreTeamOrganizeUi == null) return;
        if (ExploreTeamOrganizeUi.activeSelf == false) return;

        Miner m_target = MinerManager.Instance.MinerList[index];
        TargetMiner = m_target;
        Stat m_targetStat = m_target.gameObject.GetComponent<Stat>();
        Health m_targetHealth = m_target.gameObject.GetComponent<Health>();
        Ability m_targetAbility = m_target.gameObject.GetComponent<Ability>();

        minerNameText.text = m_target.Name;

        List<float> m_stat = m_targetStat.GetStat();
        int m_statCount = m_stat.Count;
        for (int i = 0; i < m_statCount; i++)
        {
            statTextList[i].text = m_stat[i].ToString();
        }

        List<float> m_health = m_targetHealth.GetHealth();
        int m_helathCount = healthTextList.Count;
        for (int i = 0; i < m_helathCount; i++)
        {
            healthTextList[i].text = m_health[i].ToString();
        }

        AbilityStruct abilityStruct = m_targetAbility.GetAbility();
        for(int i = 0; i < 3; i++)
        {
            List<TMP_Text> targetText = new List<TMP_Text>();
            for(int j = 2; j >= 0; j--)
            {
                switch(i)
                {
                    case 0:
                        targetText = perAbilityTextList;
                        if (abilityStruct.MinerPerfectAbilityList.Count - 1 < j) targetText[j].text = "";
                        else targetText[j].text = abilityStruct.MinerPerfectAbilityList[j].ToString();
                        break;
                    case 1:
                        targetText = posAbilityTextList;
                        if (abilityStruct.MinerPositiveAbilityList.Count - 1 < j) targetText[j].text = "";
                        else targetText[j].text = abilityStruct.MinerPositiveAbilityList[j].ToString();
                        break;
                    case 2:
                        targetText = negAbilityTextList;
                        if (abilityStruct.MinerNegativeAbilityList.Count - 1 < j) targetText[j].text = "";
                        else targetText[j].text = abilityStruct.MinerNegativeAbilityList[j].ToString();
                        break;
                }
            }
        }
    }
    public void LeftButton()
    {
        index--;
        if (index < 0) index = MinerManager.Instance.MinerList.Count - 1;
    }
    public void RightButton()
    {
        index++;
        if (index > MinerManager.Instance.MinerList.Count - 1) index = 0;
    }
    public void TeamButton()
    {
        Miner target = MinerManager.Instance.MinerList[index];
        if (ExploreMinerList.Contains(target))
        {
            ExploreMinerList.Remove(target);
        }
        else if(ExploreMinerList.Count < 5)
        {
            ExploreMinerList.Add(target);
        }
        else
        {
            // 메세지 띄우기
        }
    }
    private void Update()
    {
        MinerInfoSetting();
        ShowMinerInfo();
    }
}
