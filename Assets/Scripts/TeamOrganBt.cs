using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class TeamOrganBt : MonoBehaviour
{
    //애니메이션 0.1초짜리 넣고 모든 함수를 불러올 수 있으므로 이걸로 함수를 모두 호출할 수 있음
    //버튼 눌렀을때 해당 애니메이션을 실행하는 방식으로 구현하면 코드를 안쳐도 됨 
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
