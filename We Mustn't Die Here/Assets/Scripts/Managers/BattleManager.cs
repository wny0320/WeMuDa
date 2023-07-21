using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private static BattleManager instance;
    public static BattleManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<BattleManager>().GetComponent<BattleManager>();
                if (instance == null)
                {
                    GameObject gameObject = new GameObject();
                    gameObject.AddComponent<BattleManager>();
                    gameObject.name = typeof(BattleManager).ToString();
                    instance = gameObject.GetComponent<BattleManager>();
                }
            }
            return instance;
        }
    }
    [SerializeField]
    private GameObject battleCanvas;
    [SerializeField]
    private GameObject mainAct;
    [SerializeField]
    private GameObject fightAct;
    [SerializeField]
    private GameObject inventory;
    [SerializeField]
    private Sprite battleBackgtound;

    private List<GameObject> allyList = new List<GameObject>();
    private List<GameObject> enemyList = new List<GameObject>();
    public bool IsBattleStart = false;
    private bool isBattleEnd = false;
    private void battle()
    {
        if (IsBattleStart == false) return;
        ExploreModeToBattleMode();
        ExploreMapManager.Instance.SetExploreCanvas(false);
    }
    public void SetBattleCanvas(bool _tf)
    {
        battleCanvas.SetActive(_tf);
    }
    public void ExploreModeToBattleMode()
    {
        ExploreMapManager.Instance.SetExploreCanvas(false);
        SetBattleCanvas(true);
        BackgroundManager.Instance.SetBackground(battleBackgtound);
    }
    /// <summary>
    /// 전투시 전투 순서를 정해주는 함수
    /// </summary>
    private void battleSequence()
    {

    }
    private void battleEnemySpawn()
    {

    }
    /// <summary>
    /// 전투할 오브젝트들을 받아오는 함수
    /// </summary>
    private void getBattleObject()
    {

    }
    private void Start()
    {
        //inventory = FindObjectOfType<InventoryMng>().gameObject;
    }
    private void Update()
    {
        battle();
    }
}
