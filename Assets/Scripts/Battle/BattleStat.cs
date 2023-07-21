using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStat : MonoBehaviour
{
    [Header("Battle Stat")]
    [SerializeField]
    [Tooltip("�ִ� ü��")]
    float MaxHealth;
    [SerializeField]
    [Tooltip("���� ü��")]
    float CurHealth;
    [Tooltip("���ݷ�")]
    [SerializeField]
    float ATK;
    [SerializeField]
    [Tooltip("��Ȯ��")]
    float ACY;
    [SerializeField]
    [Tooltip("ȸ����")]
    float EVD;

    public void BattleStatSet(Stat stat)
    {
        float[] unitStat = stat.GetStat();
        MaxHealth = 100 + (unitStat[2] * 2);
        CurHealth = MaxHealth;
        ATK = 5 + (unitStat[0] * 1.5f);
        ACY = 50 + (unitStat[4] * 1f);
        EVD = 0 + (unitStat[1] * 3);
    }
}
