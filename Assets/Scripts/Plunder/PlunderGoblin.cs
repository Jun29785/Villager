using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlunderGoblin : Actor
{
    public override void Awake()
    {
        base.Awake();
    }

    public override void SetData(int Key)
    {
        var dict = DataBaseManager.Instance.tdGoblinDict[Key];
        UnitNo = dict.unitNo;
        Name = dict.Name;
        GetCoin = dict.GetCoin;
        Atk = dict.Atk;
        AtkDelay = dict.AtkDelay;
    }
}
