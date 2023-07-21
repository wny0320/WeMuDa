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
    /// ���̵��� ���� ������ ���� ĳ���ϴ� �Ա��� ���� ���� �������ִ� �Լ�
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
    /// �����ϴ� ���ε��� ��ũ��Ʈ�� �޾� ��ġ�� ����ؼ� totalMineAmount�� ���ҽ�Ű�� �Լ�
    /// </summary>
    public void Mine()
    {

    }
}
