using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MinerInfoManager : MonoBehaviour
{
    // �̱���
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

    TMP_Text minerNameText;
    List<TMP_Text> statTextList = new List<TMP_Text>();
    List<TMP_Text> healthTextList = new List<TMP_Text>();
    List<TMP_Text> perAbilityTextList = new List<TMP_Text>();
    List<TMP_Text> posAbilityTextList = new List<TMP_Text>();
    List<TMP_Text> negAbilityTextList = new List<TMP_Text>();

    public List<Miner> ExploreMinerList = new List<Miner>();
    private int index = 0;
    
    private void MinerInfoSetting()
    {
        if (GameSceneManager.Instance.NowSceneName != GameSceneManager.SceneName.CampScene)
        {
            isDataSet = false;
            return;
        }
        if (isDataSet == true) return;
        ExploreTeamOrganizeUi = GameObject.Find("ExploreTeamOrganizeBackground");

        // �ؽ�Ʈ�� ã��
        Transform m_uiTransform = ExploreTeamOrganizeUi.transform;
        minerNameText = m_uiTransform.Find("MinerName").GetComponent<TMP_Text>();

        statTextList.Add(m_uiTransform.Find("StrValue").GetComponent<TMP_Text>());
        statTextList.Add(m_uiTransform.Find("AgiValue").GetComponent<TMP_Text>());
        statTextList.Add(m_uiTransform.Find("VitValue").GetComponent<TMP_Text>());
        statTextList.Add(m_uiTransform.Find("DexValue").GetComponent<TMP_Text>());
        statTextList.Add(m_uiTransform.Find("IntValue").GetComponent<TMP_Text>());

        healthTextList.Add(m_uiTransform.Find("StressValue").GetComponent<TMP_Text>());
        healthTextList.Add(m_uiTransform.Find("HungryValue").GetComponent<TMP_Text>());
        healthTextList.Add(m_uiTransform.Find("ThirstyValue").GetComponent<TMP_Text>());

        int m_maxAbilityAmount = Ability.MaxAbilityAmount;
        int m_abilityKindAmount = Ability.abilityKindAmount;

        for(int i = 0; i < m_abilityKindAmount; i++)
        {
            List<TMP_Text> m_targetAbilityText = new List<TMP_Text>();
            for (int j = 0; j < m_maxAbilityAmount; j++)
            {
                switch(i)
                {
                    case 0:

                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                }
                //string m_targetText = "PerfectAbilityBackground/PerfectAbility" + (j + 1).ToString();
            }
        }
        perAbilityTextList.Add(m_uiTransform.Find("").GetComponent<TMP_Text>());
        perAbilityTextList.Add(m_uiTransform.Find("PerfectAbilityBackground/PerfectAbility2").GetComponent<TMP_Text>());
        perAbilityTextList.Add(m_uiTransform.Find("PerfectAbilityBackground/PerfectAbility3").GetComponent<TMP_Text>());

        posAbilityTextList.Add(m_uiTransform.Find("PositiveAbilityBackground/PositiveAbility1").GetComponent<TMP_Text>());
        posAbilityTextList.Add(m_uiTransform.Find("PositiveAbilityBackground/PositiveAbility2").GetComponent<TMP_Text>());
        posAbilityTextList.Add(m_uiTransform.Find("PositiveAbilityBackground/PositiveAbility3").GetComponent<TMP_Text>());

        negAbilityTextList.Add(m_uiTransform.Find("NegativeAbilityBackground/NegativeAbility1").GetComponent<TMP_Text>());
        negAbilityTextList.Add(m_uiTransform.Find("NegativeAbilityBackground/NegativeAbility2").GetComponent<TMP_Text>());
        negAbilityTextList.Add(m_uiTransform.Find("NegativeAbilityBackground/NegativeAbility3").GetComponent<TMP_Text>());

        ExploreTeamOrganizeUi.SetActive(false);
        isDataSet = true;
    }
    public void ShowMinerInfo()
    {
        if (GameSceneManager.Instance.NowSceneName != GameSceneManager.SceneName.CampScene) return;
        if (ExploreTeamOrganizeUi == null) return;
        if (ExploreTeamOrganizeUi.activeSelf == false) return;

        Miner m_target = MinerManager.Instance.MinerList[index];
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
            // �޼��� ����
        }
    }
    private void Update()
    {
        MinerInfoSetting();
        ShowMinerInfo();
    }
}
