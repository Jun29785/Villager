using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;
public class SpearGoblin : Goblin
{
    public override void Awake()
    {
        base.Awake();
        SetData((int)goblinEnum.창고블린);
    }
}
