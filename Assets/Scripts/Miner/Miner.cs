using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : Unit
{
    //팀에 참여했는지 확인하기 위한 bool
    private bool IsExploreAttend = false;
    public void ExploreAttendEdit(bool _tf)
    {
        List<Miner> targetList = MinerInfoManager.Instance.ExploreMinerList;
        if (_tf == false && targetList.Count < 5) targetList.Add(this); // 참여해있지 않으며 수가 5보다 작다면 추가
        else targetList.Remove(this); // 참여했거나 5보다 클 경우
        IsExploreAttend = _tf;
    }
    public bool ReturnExploreAttend()
    {
        return IsExploreAttend;
    }
}
