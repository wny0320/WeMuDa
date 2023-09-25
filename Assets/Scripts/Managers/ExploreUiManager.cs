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
    // Ui�� �⺻���� �����͵�
    private Canvas minerInfoCanvas;
    private GameObject minerInfoUI;
    private Button infoExitBt;
    private Button infoBt;

    // Ui �����͸� ã�Ҵ��� �˷��ִ� bool
    private bool isDataSet = false;

    // ������ Sync�� ���� �����͵�
    private List<GameObject> minerStatusInfo = new List<GameObject>();
    private bool isSyncWorked = false;
    // ���� Ȱ��ȭ �� Ui�� ����, �� ���� Ž������ Miner�� ��
    private int activatedNum = 0;
    // ExploreMapManager�� �ִ� CanExploreMinerCount�� ���� ���� ������ ��
    private const int maxUiNum = 5;

    // minerStatusInfo ���� Image, ������ Sprite�� �־��ָ� ��
    private List<Image> minerStatusImage;
    // minerStatusInfo ���� hpGauge
    private List<Image> hpGauge;
    // minerStatusInfo ���� stressGauge
    private List<Image> stressGauge;
    // minerStatusInfo ���� hungerGauge
    private List<Image> hungerGauge;
    // minerStatusInfo ���� thirstyGauge
    private List<Image> thirstyGauge;
    // minerStatusInf ���� oxygenGauge
    private List<Image> oxygenGauge;

    // �ڷ�ƾ�� �۵��ϰ� �ִ��� �Ǻ�
    private Coroutine exploreCo = null;

    private void findData()
    {
        // ĵ���� ã��
        minerInfoCanvas = GameObject.Find("MinerInfoCanvas").GetComponent<Canvas>();
        // Ui ������Ʈ ã��
        minerInfoUI = minerInfoCanvas.transform.Find("MinerInfoUI").gameObject;

        // Prefabȭ �Ǿ� �ִ� ������Ʈ ã��
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
    /// ��ư�� ����� �߰�, ����� ui ��Ȱ��ȭ, �ݱ� �����ܿ� ����� ��ũ��Ʈ
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
        // ���� Scene�� ExploreScene�� �ƴҰ�� �Լ� ����� �Բ� dataSet�� false
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
    /// ���� ����â�� ����ȭ �����ִ� �Լ�
    /// </summary>
    private void minerStatusInfoSync() // ���� ���ε����͸� �Ѱ����� �ʾ���
    {
        if (GameSceneManager.Instance.NowSceneName != GameSceneManager.SceneName.ExploreScene) return;
        if (isDataSet == false) return;
        if (minerInfoUI.activeSelf == false) return;

        // ����Ʈ ���� ����, �̱��� ������ �� �ϱ� ����
        List<Miner> m_exploringMinerList = ExploreMapManager.Instance.ExploringMinerList;

        // ���� Ž���ϴ� ������ ��
        int activatedNum = m_exploringMinerList == null ? 0 : m_exploringMinerList.Count;

        if (isSyncWorked == false)
        {
            // ����Ʈ�� �ʱ�ȭ
            minerStatusImage = new List<Image>();
            hpGauge = new List<Image>();
            stressGauge = new List<Image>();
            hungerGauge = new List<Image>();
            oxygenGauge = new List<Image>();

            for (int i = 0; i < activatedNum; i++)
            {
                // minerImage ã��
                Image m_targetImage = minerStatusInfo[i].transform.Find("MinerImage").GetComponent<Image>();
                minerStatusImage.Add(m_targetImage);

                // minerHpGauge ã��
                Image m_targetHpGauge = minerStatusInfo[i].transform.Find("MinerHpBar/MinerHpGauge").GetComponent<Image>();
                hpGauge.Add(m_targetHpGauge);

                // minerStressGauge ã��
                Image m_targetStressGauge = minerStatusInfo[i].transform.Find("MinerStressBar/MinerStressGauge").GetComponent<Image>();
                stressGauge.Add(m_targetStressGauge);

                // minerHungerGauge ã��
                Image m_targetHungerGauge = minerStatusInfo[i].transform.Find("MinerHungerBar/MinerHungerGauge").GetComponent<Image>();
                hungerGauge.Add(m_targetHungerGauge);

                // minerThirstyGauge ã��
                Image m_targetThirstyGauge = minerStatusInfo[i].transform.Find("MinerThirstyBar/MinerThirstyGauge").GetComponent<Image>();
                thirstyGauge.Add(m_targetThirstyGauge);

                // minerOxygenGauge ã��
                Image m_targetOxygenGauge = minerStatusInfo[i].transform.Find("MinerOxygenBar/MinerOxygenGauge").GetComponent<Image>();
                oxygenGauge.Add(m_targetOxygenGauge);
            }
            isSyncWorked = true;
        }
        // Sync�ϴ� ����
        for(int i = 0; i < activatedNum; i++)
        {
            // ���� sprite�� ����
            minerStatusImage[i].sprite = m_exploringMinerList[i].gameObject.GetComponent<SpriteRenderer>().sprite;

            // Sync�� �ʿ��� ������ �ޱ�
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
