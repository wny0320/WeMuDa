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
    [SerializeField,Tooltip("������ ���� ��ҷ�")]
    public float Oxygen;
    private int m_oxygenLackTurn = 0;
    /// <summary>
    /// ����� ���� �ʱ�ȭ���ִ� �Լ�, Ư���� ������ ����
    /// </summary>
    public void OxygenReset()
    {
        if(true)//���߿� Ư���� �� �ڸ�
        {
            Oxygen = 100; //Ư���� ���� ����� ���� �־������
        }
        else
        {
            Oxygen = 100;
        }
    }

    /// <summary>
    /// ����� ���� �������ִ� �Լ�
    /// </summary>
    /// <param name="_amount">�����ǰų� ���ҵǴ� ��, ������ ����� ����</param>
    public void OxygenManage(float _amount)
    {
        Oxygen += _amount;
    }

    private void checkOxygen()
    {
        if(Oxygen <= 0f)
        {
            //�� ���°� 2�� �̻� ���ӽ� ���� ���
        }
        else if(Oxygen <= 15f)
        {
            //��� �������� ���� �г�Ƽ 2, �� ���� ���� �ʱ�ȭ
        }
        else if(Oxygen <= 30f)
        {
            //��� �������� ���� �г�Ƽ 1, �� ���� ���� �ʱ�ȭ �� �г�Ƽ 2 ����
        }
        else
        {
            //�г�Ƽ ���� �� �� ���� ���� �ʱ�ȭ
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
