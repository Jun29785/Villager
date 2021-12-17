using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

public class Bill : Villager
{
    public override void Awake()
    {
        base.Awake();
        SetData((int)VillagerEnum.ºô);
    }
}
