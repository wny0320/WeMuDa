using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MinerInfoManager : MonoBehaviour
{
    // ΩÃ±€≈Ê
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

        // ≈ÿΩ∫∆ÆµÈ √£±‚
        minerNameText = ExploreTeamOrganizeUi.transform.Find("MinerName").GetComponent<TMP_Text>();

        statTextList.Add(ExploreTeamOrganizeUi.transform.Find("StrValue").GetComponent<TMP_Text>());
        statTextList.Add(ExploreTeamOrganizeUi.transform.Find("AgiValue").GetComponent<TMP_Text>());
        statTextList.Add(ExploreTeamOrganizeUi.transform.Find("VitValue").GetComponent<TMP_Text>());
        statTextList.Add(ExploreTeamOrganizeUi.transform.Find("DexValue").GetComponent<TMP_Text>());
        statTextList.Add(ExploreTeamOrganizeUi.transform.Find("IntValue").GetComponent<TMP_Text>());

        healthTextList.Add(ExploreTeamOrganizeUi.transform.Find("StressValue").GetComponent<TMP_Text>());
        healthTextList.Add(ExploreTeamOrganizeUi.transform.Find("HungryValue").GetComponent<TMP_Text>());
        healthTextList.Add(ExploreTeamOrganizeUi.transform.Find("ThirstyValue").GetComponent<TMP_Text>());

        perAbilityTextList.Add(ExploreTeamOrganizeUi.transform.Find("PerfectAbilityBackground/PerfectAbility1").GetComponent<TMP_Text>());
        perAbilityTextList.Add(ExploreTeamOrganizeUi.transform.Find("PerfectAbilityBackground/PerfectAbility2").GetComponent<TMP_Text>());
        perAbilityTextList.Add(ExploreTeamOrganizeUi.transform.Find("PerfectAbilityBackground/PerfectAbility3").GetComponent<TMP_Text>());

        posAbilityTextList.Add(ExploreTeamOrganizeUi.transform.Find("PositiveAbilityBackground/PositiveAbility1").GetComponent<TMP_Text>());
        posAbilityTextList.Add(ExploreTeamOrganizeUi.transform.Find("PositiveAbilityBackground/PositiveAbility2").GetComponent<TMP_Text>());
        posAbilityTextList.Add(ExploreTeamOrganizeUi.transform.Find("PositiveAbilityBackground/PositiveAbility3").GetComponent<TMP_Text>());

        negAbilityTextList.Add(ExploreTeamOrganizeUi.transform.Find("NegativeAbilityBackground/NegativeAbility1").GetComponent<TMP_Text>());
        negAbilityTextList.Add(ExploreTeamOrganizeUi.transform.Find("NegativeAbilityBackground/NegativeAbility2").GetComponent<TMP_Text>());
        negAbilityTextList.Add(ExploreTeamOrganizeUi.transform.Find("NegativeAbilityBackground/NegativeAbility3").GetComponent<TMP_Text>());

        ExploreTeamOrganizeUi.SetActive(false);
        isDataSet = true;
    }
    public void ShowMinerInfo()
    {
        if (GameSceneManager.Instance.NowSceneName != GameSceneManager.SceneName.CampScene) return;
        if (ExploreTeamOrganizeUi == null) return;
        if (ExploreTeamOrganizeUi.activeSelf == false) return;
        Miner target = MinerManager.Instance.MinerList[index];
        Stat targetStat = target.gameObject.GetComponent<Stat>();
        Health targetHealth = target.gameObject.GetComponent<Health>();
        Ability targetAbility = target.gameObject.GetComponent<Ability>();

        minerNameText.text = target.Name;
        float[] stat = targetStat.GetStat();
        for(int i = 0; i < stat.Length; i++)
        {
            statTextList[i].text = stat[i].ToString();
        }
        float[] health = targetHealth.GetHealth();
        for(int i = 0; i < healthTextList.Count; i++)
        {
            healthTextList[i].text = health[i].ToString();
        }
        AbilityStruct abilityStruct = targetAbility.GetAbility();
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
            // ∏ﬁºº¡ˆ ∂ÁøÏ±‚
        }
    }
    private void Update()
    {
        MinerInfoSetting();
        ShowMinerInfo();
    }
}
