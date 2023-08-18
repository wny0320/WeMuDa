using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ExploreUiManager : MonoBehaviour
{
    private static ExploreUiManager instance;
    public static ExploreUiManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.Find("Managers").AddComponent<ExploreUiManager>();
            }
            return instance;
        }
    }
    // Ui의 기본적인 데이터들
    private Canvas oxygenCanvas;
    private GameObject oxygenInfoUi;
    private Button infoExitBt;

    // Ui 데이터를 찾았는지 알려주는 bool
    private bool isDataSet = false;

    // 데이터 Sync를 위한 데이터들
    private List<GameObject> minerStatusInfo = new List<GameObject>();
    private bool isSyncWorked = false;
    // 현재 활성화 된 Ui의 갯수, 즉 현재 탐험중인 Miner의 수
    private int activatedNum = 0;
    // ExploreMapManager에 있는 CanExploreMinerCount와 같은 수로 지정할 것
    private const int maxUiNum = 5;

    // minerStatusInfo 내의 Image, 광부의 Sprite를 넣어주면 됨
    private List<Image> minerStatusImage;
    // minerStatusInfo 내의 hpGauge
    private List<Image> hpGauge;
    // minerStatusInfo 내의 stressGauge
    private List<Image> stressGauge;
    // minerStatusInfo 내의 hungerGauge
    private List<Image> hungerGauge;
    // minerStatusInfo 내의 thirstyGauge
    private List<Image> thirstyGauge;
    // minerStatusInf 내의 oxygenGauge
    private List<Image> oxygenGauge;

    private void findData()
    {
        // 캔버스 찾기
        oxygenCanvas = GameObject.Find("OxygenCanvas").GetComponent<Canvas>();
        // Ui 오브젝트 찾기
        oxygenInfoUi = oxygenCanvas.transform.Find("OxygenInfoUI").gameObject;

        // Prefab화 되어 있는 오브젝트 찾기
        minerStatusInfo = new List<GameObject>();

        int m_count = ExploreMapManager.CanExploreMinerCount;
        for (int i = 0; i < m_count; i++)
        {
            string targetName = "MinerStatusInfo" + i.ToString();
            GameObject target = oxygenInfoUi.transform.Find("LayoutGroup/" + targetName).gameObject;
            minerStatusInfo.Add(target);
        }

        infoExitBt = oxygenInfoUi.transform.Find("InfoExitIcon").GetComponent<Button>();
    }

    /// <summary>
    /// 버튼에 기능을 추가, 기능은 ui 비활성화
    /// </summary>
    private void buttonSet()
    {
        infoExitBt.onClick.AddListener(() =>
        {
            oxygenInfoUi.SetActive(false);
        });
    }
    private IEnumerator exploreUiSet()
    {
        // 현재 Scene이 ExploreScene이 아닐경우 함수 종료와 함께 dataSet을 false
        if (GameSceneManager.Instance.NowSceneName != GameSceneManager.SceneName.ExploreScene)
        {
            isDataSet = false;
            yield return null;
        }
        if (isDataSet == true) yield return null;
        yield return new WaitForEndOfFrame();
        findData();
        buttonSet();

        isDataSet = true;
    }

    /// <summary>
    /// 광부 정보창을 동기화 시켜주는 함수
    /// </summary>
    private void minerStatusInfoSync()
    {
        if (GameSceneManager.Instance.NowSceneName != GameSceneManager.SceneName.ExploreScene) return;
        if (isDataSet == false) return;
        if (oxygenInfoUi.activeSelf == false) return;

        // 리스트 얕은 복사, 싱글톤 참조를 덜 하기 위함
        List<GameObject> m_exploringMinerList = ExploreMapManager.Instance.ExploringMinerList;

        // 현재 탐험하는 광부의 수
        int activatedNum = m_exploringMinerList.Count;

        if (isSyncWorked == false)
        {
            // 리스트들 초기화
            minerStatusImage = new List<Image>();
            hpGauge = new List<Image>();
            stressGauge = new List<Image>();
            hungerGauge = new List<Image>();
            oxygenGauge = new List<Image>();

            for (int i = 0; i < activatedNum; i++)
            {
                // minerImage 찾기
                Image m_targetImage = minerStatusInfo[i].transform.Find("MinerImage").GetComponent<Image>();
                minerStatusImage.Add(m_targetImage);

                // minerHpGauge 찾기
                Image m_targetHpGauge = minerStatusInfo[i].transform.Find("MinerHpBar/MinerHpGauge").GetComponent<Image>();
                hpGauge.Add(m_targetHpGauge);

                // minerStressGauge 찾기
                Image m_targetStressGauge = minerStatusInfo[i].transform.Find("MinerStressBar/MinerStressGauge").GetComponent<Image>();
                stressGauge.Add(m_targetStressGauge);

                // minerHungerGauge 찾기
                Image m_targetHungerGauge = minerStatusInfo[i].transform.Find("MinerHungerBar/MinerHungerGauge").GetComponent<Image>();
                hungerGauge.Add(m_targetHungerGauge);

                // minerThirstyGauge 찾기
                Image m_targetThirstyGauge = minerStatusInfo[i].transform.Find("MinerThirstyBar/MinerThirstyGauge").GetComponent<Image>();
                thirstyGauge.Add(m_targetThirstyGauge);

                // minerOxygenGauge 찾기
                Image m_targetOxygenGauge = minerStatusInfo[i].transform.Find("MinerOxygenBar/MinerOxygenGauge").GetComponent<Image>();
                oxygenGauge.Add(m_targetOxygenGauge);
            }
            isSyncWorked = true;
        }
        // Sync하는 과정
        for(int i = 0; i < activatedNum; i++)
        {
            // 광부 sprite로 변경
            minerStatusImage[i].sprite = m_exploringMinerList[i].GetComponent<SpriteRenderer>().sprite;

            // Sync에 필요한 데이터 받기
            BattleStat m_battleStat = m_exploringMinerList[i].GetComponent<BattleStat>();
            Health m_health = m_exploringMinerList[i].GetComponent<Health>();

            // Sync
            List<float> m_battleStatData = m_battleStat.ReturnBattleStats();
            float m_maxHp = m_battleStatData[(int)BattleStat.ReturnedStatIndex.maxHealth];
            float m_curHp = m_battleStatData[(int)BattleStat.ReturnedStatIndex.curHealth];
            hpGauge[i].fillAmount = m_curHp / m_maxHp;

            List<float> m_maxHealth = m_health.GetMaxHealth();
            List<float> m_curHealth = m_health.GetHealth();
            //stressGauge[i].fillAmount = m_curHealth.
        }
        for(int i = activatedNum; i < maxUiNum; i++)
        {
            m_exploringMinerList[i].SetActive(false);
        }
    }
    private void Start()
    {
        StartCoroutine(exploreUiSet());
    }
    void Update()
    {
        minerStatusInfoSync();
    }
}
