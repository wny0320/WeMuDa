using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineManager : MonoBehaviour
{
    
    private static MineManager instance;
    public static MineManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<MineManager>();
                if(instance == null)
                {
                    GameObject gameObject = new GameObject();
                    instance = gameObject.AddComponent<MineManager>();
                    gameObject.name = typeof(MineManager).ToString();
                }
            }
            return instance;
        }
    }
    private float totalMineAmount;
    [System.Serializable]
    private struct mineAmountForLevel
    {
        public float easyAmount;
        public float normalAmount;
        public float hardAmount;
        public float hardCoreAmount;
    }
    [SerializeField]
    private mineAmountForLevel levels = new mineAmountForLevel();
    /// <summary>
    /// 난이도에 따라 광질로 전부 캐야하는 입구의 광물 양을 설정해주는 함수
    /// </summary>
    public void SetMineAmount()
    {
        switch(GameManager.Instance.Level)
        {
            case GameManager.LevelEnum.easy:
                totalMineAmount = levels.easyAmount;
                break;
            case GameManager.LevelEnum.normal:
                totalMineAmount = levels.normalAmount;
                break;
            case GameManager.LevelEnum.hard:
                totalMineAmount = levels.hardAmount;
                break;
            case GameManager.LevelEnum.hardCore:
                totalMineAmount = levels.hardCoreAmount;
                break;
        }
    }
    /// <summary>
    /// 광질하는 광부들의 스크립트를 받아 수치를 계산해서 totalMineAmount를 감소시키는 함수
    /// </summary>
    public void Mine()
    {

    }
}
