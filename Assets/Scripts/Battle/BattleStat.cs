using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStat : MonoBehaviour
{
    public enum ReturnedStatIndex
    {
        maxHealth,
        curHealth,
        atk,
        acy,
        evd,

    }
    [Header("Battle Stat")]
    // 최대 체력
    private float maxHealth;
    // 현재 체력
    private float curHealth;
    // 공격력
    private float atk;
    // 정확도
    private float acy;
    // 회피율
    private float evd;

    public void BattleStatSet(Stat stat)
    {
        List<float> unitStat = stat.GetStat();
        maxHealth = 100 + (unitStat[2] * 2);
        curHealth = maxHealth;
        atk = 5 + (unitStat[0] * 1.5f);
        acy = 50 + (unitStat[4] * 1f);
        evd = 0 + (unitStat[1] * 3);
    }

    public List<float> ReturnBattleStats()
    {
        return new List<float> {maxHealth, curHealth, atk, acy, evd};
    }
}
