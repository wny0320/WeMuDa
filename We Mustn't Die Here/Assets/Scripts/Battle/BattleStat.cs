using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStat : MonoBehaviour
{
    [Header("Battle Stat")]
    [SerializeField]
    [Tooltip("최대 체력")]
    float MaxHealth;
    [SerializeField]
    [Tooltip("현재 체력")]
    float CurHealth;
    [Tooltip("공격력")]
    [SerializeField]
    float ATK;
    [SerializeField]
    [Tooltip("정확도")]
    float ACY;
    [SerializeField]
    [Tooltip("회피율")]
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
