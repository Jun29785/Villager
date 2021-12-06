using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

public class DagerGoblin : Goblin
{
    public override void Awake()
    {
        base.Awake();
        SetData((int)goblinEnum.단검고블린);
    }
}
