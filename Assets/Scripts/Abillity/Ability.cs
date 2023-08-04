using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    private enum probability
    {
        NegativeProbability = 2,
        PositiveProbability = 3,
        PerfectProbability = 5,
    }

    private AbilityStruct abilityStruct = new AbilityStruct();

    private int PerAbilNum = System.Enum.GetValues(typeof(PerAbilEnum)).Length;
    private int PosAbilNum = System.Enum.GetValues(typeof(PosAbilEnum)).Length;
    private int NegAbilNum = System.Enum.GetValues(typeof(NegAbilEnum)).Length;

    public const int MaxAbilityAmount = 3;
    public const int abilityKindAmount = 3;

    /// <summary>
    /// 6면체 주사위를 굴려 1이면 부정적인 재능, 2~3이라면 재능 얻기에 실패, 4~5라면 일반적인 재능, 6이라면 완벽한 재능을 얻게하는 함수
    /// <summary>
    public void RandomAbillity()
    {
        abilityStruct.MinerNegativeAbilityList = new List<NegAbilEnum>();
        abilityStruct.MinerPerfectAbilityList = new List<PerAbilEnum>();
        abilityStruct.MinerPositiveAbilityList = new List<PosAbilEnum>();
        for (int i = 0; i < MaxAbilityAmount; i++)
        {
            int m_dice = Random.Range(1, 7);
            if (m_dice < (int)probability.NegativeProbability)
            {
                m_dice = Random.Range(0, NegAbilNum);
                while (abilityStruct.MinerNegativeAbilityList.IndexOf((NegAbilEnum)m_dice) != -1)
                {
                    m_dice = Random.Range(0, NegAbilNum);
                }
                abilityStruct.MinerNegativeAbilityList.Add((NegAbilEnum)m_dice);
            }
            else if (m_dice > (int)probability.PerfectProbability)
            {
                m_dice = Random.Range(0, PerAbilNum);
                while (abilityStruct.MinerPerfectAbilityList.IndexOf((PerAbilEnum)m_dice) != -1)
                {
                    m_dice = Random.Range(0, PerAbilNum);
                }
                abilityStruct.MinerPerfectAbilityList.Add((PerAbilEnum)m_dice);
            }
            else if (m_dice > (int)probability.PositiveProbability)
            {
                m_dice = Random.Range(0, PosAbilNum);
                while (abilityStruct.MinerPositiveAbilityList.IndexOf((PosAbilEnum)m_dice) != -1)
                {
                    m_dice = Random.Range(0, PosAbilNum);
                }
                abilityStruct.MinerPositiveAbilityList.Add((PosAbilEnum)m_dice);
            }
        }
    }
    public AbilityStruct GetAbility()
    {
        return abilityStruct;
    }
}
