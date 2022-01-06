using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

public class 현욱 : Villager
{
    public override void Awake()
    {
        base.Awake();
        SetData((int)VillagerEnum.현욱);
    }
}
