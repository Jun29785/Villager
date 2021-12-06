using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Abstract parent class inhibit
public class Goblin : Actor
{
    public override void Awake()
    {
        base.Awake();

    }

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();

    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void SetData(int Key)
    {
        var dict = DataBaseManager.Instance.tdGoblinDict[Key];
        UnitNo = dict.unitNo;
        Name = dict.Name;
        GetCoin = dict.GetCoin;
        Atk = dict.Atk;
        AtkDelay = dict.AtkDelay;

        throw new System.NotImplementedException();
    }
}
