using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : Unit
{
    //���� �����ߴ��� Ȯ���ϱ� ���� bool
    private bool IsExploreAttend = false;
    public void ExploreAttendEdit(bool _tf)
    {
        List<Miner> targetList = MinerInfoManager.Instance.ExploreMinerList;
        if (_tf == false && targetList.Count < 5) targetList.Add(this); // ���������� ������ ���� 5���� �۴ٸ� �߰�
        else targetList.Remove(this); // �����߰ų� 5���� Ŭ ���
        IsExploreAttend = _tf;
    }
    public bool ReturnExploreAttend()
    {
        return IsExploreAttend;
    }
}
