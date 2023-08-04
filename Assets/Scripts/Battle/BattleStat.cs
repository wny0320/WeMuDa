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
    // �ִ� ü��
    private float maxHealth;
    // ���� ü��
    private float curHealth;
    // ���ݷ�
    private float atk;
    // ��Ȯ��
    private float acy;
    // ȸ����
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
