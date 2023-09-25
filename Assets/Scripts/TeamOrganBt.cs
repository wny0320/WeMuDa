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
    public enum funcList
    {
        TeamPlusMinusBt,
        TeamConfirmBt,
    }
    private Button button;
    public funcList toDoAction;
    private Action action;
    //�ӽ÷� �����͸� ���� ���� �ڵ�
    [SerializeField] List<Miner> one = new List<Miner>();
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            Invoke(toDoAction.ToString(), 0);
        });
        one = MinerInfoManager.Instance.ExploreMinerList;
    }
    public void TeamPlusMinusBt()
    {
        Miner m_target = MinerInfoManager.Instance.TargetMiner;
        bool m_tf = m_target.ReturnExploreAttend(); // Ÿ�� ���ΰ� �������ִ��� ��ȯ
        if (m_tf == true) m_target.ExploreAttendEdit(true);
        else
        {
            m_target.ExploreAttendEdit(false);
        }
    }
    public void TeamConfirmBt() // ���� �����غ��� ������ �� ����� ���� �κ��̶� ��ư���� �ʿ䰡 ������ ��
    {
        ExploreMapManager.Instance.StartExplore();
    }
}
