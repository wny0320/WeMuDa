using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenUnit : MonoBehaviour
{
    private enum m_oxygenPenaltyEnum
    {
        stressUp,
        maxHealthDown,
        strDown,
        intDown,
        vitDown,
        agiDown,
        dexDown,
    }
    [SerializeField,Tooltip("유닛의 남은 산소량")]
    public float Oxygen;
    private int m_oxygenLackTurn = 0;
    /// <summary>
    /// 산소의 양을 초기화해주는 함수, 특성에 영향을 받음
    /// </summary>
    public void OxygenReset()
    {
        if(true)//나중에 특성이 들어갈 자리
        {
            Oxygen = 100; //특성에 따른 산소의 값을 넣어줘야함
        }
        else
        {
            Oxygen = 100;
        }
    }

    /// <summary>
    /// 산소의 양을 관리해주는 함수
    /// </summary>
    /// <param name="_amount">증가되거나 감소되는 양, 음수와 양수로 구분</param>
    public void OxygenManage(float _amount)
    {
        Oxygen += _amount;
    }

    private void checkOxygen()
    {
        if(Oxygen <= 0f)
        {
            //현 상태가 2턴 이상 지속시 광부 사망
        }
        else if(Oxygen <= 15f)
        {
            //산소 부족으로 인한 패널티 2, 턴 지속 변수 초기화
        }
        else if(Oxygen <= 30f)
        {
            //산소 부족으로 인한 패널티 1, 턴 지속 변수 초기화 및 패널티 2 삭제
        }
        else
        {
            //패널티 삭제 및 턴 지속 변수 초기화
        }
    }

    private void addOxygenPenalty(m_oxygenPenaltyEnum _penalty)
    {
        switch(_penalty)
        {
            case m_oxygenPenaltyEnum.stressUp:
                break;
            case m_oxygenPenaltyEnum.maxHealthDown:
                break;
            case m_oxygenPenaltyEnum.strDown:
                break;
            case m_oxygenPenaltyEnum.intDown:
                break;
            case m_oxygenPenaltyEnum.vitDown:
                break;
            case m_oxygenPenaltyEnum.agiDown:
                break;
            case m_oxygenPenaltyEnum.dexDown:
                break;
        }
    }
}
