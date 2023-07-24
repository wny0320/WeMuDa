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
    private bool isUiDataSet = false;

    TMP_Text minerNameText;
    List<TMP_Text> statTextList;
    List<TMP_Text> healthTextList;
    List<TMP_Text> perAbilityTextList;
    List<TMP_Text> posAbilityTextList;
    List<TMP_Text> negAbilityTextList;

    public List<Miner> ExploreMinerList = new List<Miner>();
    private int index = 0;
    
    private void findExploreTeamOrganizeUi()
    {
        if (GameSceneManager.Instance.NowSceneName != GameSceneManager.SceneName.CampScene)
        {
            isUiDataSet = false;
            return;
        }
        if (isUiDataSet == true) return;
        ExploreTeamOrganizeUi = GameObject.Find("ExploreTeamOrganizeBackground");
        ExploreTeamOrganizeUi.SetActive(false);
        isUiDataSet = true;
    }
    public void ShowMinerInfo()
    {
        if (GameSceneManager.Instance.NowSceneName != GameSceneManager.SceneName.CampScene) return;
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
        findExploreTeamOrganizeUi();
        ShowMinerInfo();
    }
}
