using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class TeamOrganBt : MonoBehaviour
{
    //�ִϸ��̼� 0.1��¥�� �ְ� ��� �Լ��� �ҷ��� �� �����Ƿ� �̰ɷ� �Լ��� ��� ȣ���� �� ����
    //��ư �������� �ش� �ִϸ��̼��� �����ϴ� ������� �����ϸ� �ڵ带 ���ĵ� �� 
    private enum funcList
    {
        TeamPlusMinusBt,

    }
    private Button button;
    private funcList toDoAction;
    private Action action;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            Invoke(toDoAction.ToString(), 0);
        });
    }
    public void TeamPlusMinusBt()
    {
        Miner m_target = MinerInfoManager.Instance.TargetMiner;
        bool m_tf = m_target.ReturnExploreAttend();
        if (m_tf == true) m_target.ExploreAttendEdit(true);
        else m_target.ExploreAttendEdit(false);
    }
}
