using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : Unit
{
    private bool IsExploreAttend = false;
    public void ExploreAttendEdit(bool _tf)
    {
        IsExploreAttend = _tf;
    }
    public bool ReturnExploreAttend()
    {
        return IsExploreAttend;
    }
}
