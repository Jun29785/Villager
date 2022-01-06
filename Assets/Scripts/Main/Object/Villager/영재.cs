using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

public class 영재 : Villager
{
    public override void Awake()
    {
        base.Awake();
        SetData((int)VillagerEnum.영재);
    }
}
