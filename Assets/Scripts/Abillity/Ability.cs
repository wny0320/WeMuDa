using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    enum probability
    {
        NegativeProbability = 2,
        PositiveProbability = 3,
        PerfectProbability = 5,
    }
    [Header("Miner Abillity")]
    [Header("광부의 재능, 광부마다 재능이 달라 행동에 보정이 붙는다.")]
    [SerializeField]
    AbilityStruct abilityStruct = new AbilityStruct();

    int PerAbilNum = System.Enum.GetValues(typeof(PerAbilEnum)).Length;
    int PosAbilNum = System.Enum.GetValues(typeof(PosAbilEnum)).Length;
    int NegAbilNum = System.Enum.GetValues(typeof(NegAbilEnum)).Length;

    /// <summary>
    /// 6면체 주사위를 굴려 1이면 부정적인 재능, 2~3이라면 재능 얻기에 실패, 4~5라면 일반적인 재능, 6이라면 완벽한 재능을 얻게하는 함수
    /// <summary>
    public void RandomAbillity()
    {
        abilityStruct.MinerNegativeAbilityList = new List<NegAbilEnum>();
        abilityStruct.MinerPerfectAbilityList = new List<PerAbilEnum>();
        abilityStruct.MinerPositiveAbilityList = new List<PosAbilEnum>();
        for (int i = 0; i < 3; i++)
        {
            int dice = Random.Range(1, 7);
            if (dice < (int)probability.NegativeProbability)
            {
                dice = Random.Range(0, NegAbilNum);
                while (abilityStruct.MinerNegativeAbilityList.IndexOf((NegAbilEnum)dice) != -1)
                {
                    dice = Random.Range(0, NegAbilNum);
                }
                abilityStruct.MinerNegativeAbilityList.Add((NegAbilEnum)dice);
            }
            else if (dice > (int)probability.PerfectProbability)
            {
                dice = Random.Range(0, PerAbilNum);
                while (abilityStruct.MinerPerfectAbilityList.IndexOf((PerAbilEnum)dice) != -1)
                {
                    dice = Random.Range(0, PerAbilNum);
                }
                abilityStruct.MinerPerfectAbilityList.Add((PerAbilEnum)dice);
            }
            else if (dice > (int)probability.PositiveProbability)
            {
                dice = Random.Range(0, PosAbilNum);
                while (abilityStruct.MinerPositiveAbilityList.IndexOf((PosAbilEnum)dice) != -1)
                {
                    dice = Random.Range(0, PosAbilNum);
                }
                abilityStruct.MinerPositiveAbilityList.Add((PosAbilEnum)dice);
            }
        }
    }
    public AbilityStruct GetAbility()
    {
        return abilityStruct;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
