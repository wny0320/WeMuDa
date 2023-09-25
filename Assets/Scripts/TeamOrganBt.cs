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
    public enum funcList
    {
        TeamPlusMinusBt,
        TeamConfirmBt,
    }
    private Button button;
    public funcList toDoAction;
    private Action action;
    //임시로 데이터를 보기 위한 코드
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
        bool m_tf = m_target.ReturnExploreAttend(); // 타겟 광부가 참여해있는지 반환
        if (m_tf == true) m_target.ExploreAttendEdit(true);
        else
        {
            m_target.ExploreAttendEdit(false);
        }
    }
    public void TeamConfirmBt() // 지금 생각해보면 어차피 턴 종료시 들어가는 부분이라 버튼으로 필요가 없을듯 함
    {
        ExploreMapManager.Instance.StartExplore();
    }
}
