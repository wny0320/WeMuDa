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
    private Canvas minerInfoCanvas;
    private GameObject minerInfoUI;
    private Button infoExitBt;
    private Button infoBt;

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

    // 코루틴이 작동하고 있는지 판별
    private Coroutine exploreCo = null;

    private void findData()
    {
        // 캔버스 찾기
        minerInfoCanvas = GameObject.Find("MinerInfoCanvas").GetComponent<Canvas>();
        // Ui 오브젝트 찾기
        minerInfoUI = minerInfoCanvas.transform.Find("MinerInfoUI").gameObject;

        // Prefab화 되어 있는 오브젝트 찾기
        minerStatusInfo = new List<GameObject>();

        int m_count = ExploreMapManager.CanExploreMinerCount;
        for (int i = 0; i < m_count; i++)
        {
            string targetName = "MinerStatusInfo" + i.ToString();
            GameObject target = minerInfoUI.transform.Find("LayoutGroup/" + targetName).gameObject;
            minerStatusInfo.Add(target);
        }

        infoExitBt = minerInfoUI.transform.Find("InfoExitIcon").GetComponent<Button>();
        infoBt = GameObject.Find("ExploreUICanvas/InfoButton").GetComponent<Button>();
        minerInfoUI.SetActive(false);
    }

    /// <summary>
    /// 버튼에 기능을 추가, 기능은 ui 비활성화, 닫기 아이콘에 사용할 스크립트
    /// </summary>
    private void buttonSet()
    {
        infoExitBt.onClick.AddListener(() =>
        {
            minerInfoUI.SetActive(false);
        });
        infoBt.onClick.AddListener(() =>
        {
            if (minerInfoUI.activeSelf == true) minerInfoUI.SetActive(false);
            else minerInfoUI.SetActive(true);
        });
    }
    private IEnumerator exploreUiSet()
    {
        // 현재 Scene이 ExploreScene이 아닐경우 함수 종료와 함께 dataSet을 false
        if (GameSceneManager.Instance.NowSceneName != GameSceneManager.SceneName.ExploreScene)
        {
            isDataSet = false;
            exploreCo = null;
            yield break;
        }
        if (isDataSet == true)
        {
            exploreCo = null;
            yield break;
        }
        yield return new WaitForFixedUpdate();
        findData();
        buttonSet();

        isDataSet = true;
        exploreCo = null;
    }
    public void ExploreMinerInfoUiOnOff(bool _tf)
    {
        minerInfoUI.SetActive(_tf);
    }

    /// <summary>
    /// 광부 정보창을 동기화 시켜주는 함수
    /// </summary>
    private void minerStatusInfoSync() // 아직 광부데이터를 넘겨주지 않았음
    {
        if (GameSceneManager.Instance.NowSceneName != GameSceneManager.SceneName.ExploreScene) return;
        if (isDataSet == false) return;
        if (minerInfoUI.activeSelf == false) return;

        // 리스트 얕은 복사, 싱글톤 참조를 덜 하기 위함
        List<Miner> m_exploringMinerList = ExploreMapManager.Instance.ExploringMinerList;

        // 현재 탐험하는 광부의 수
        int activatedNum = m_exploringMinerList == null ? 0 : m_exploringMinerList.Count;

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
            minerStatusImage[i].sprite = m_exploringMinerList[i].gameObject.GetComponent<SpriteRenderer>().sprite;

            // Sync에 필요한 데이터 받기
            BattleStat m_battleStat = m_exploringMinerList[i].gameObject.GetComponent<BattleStat>();
            Health m_health = m_exploringMinerList[i].gameObject.GetComponent<Health>();

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
            minerStatusInfo[i].gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        minerStatusInfoSync();
        if(exploreCo == null) exploreCo = StartCoroutine(exploreUiSet());
    }
}
