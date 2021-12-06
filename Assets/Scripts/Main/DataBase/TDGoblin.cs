using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System;

public class TDGoblin : TableBase
{
    public int unitNo;
    public string Name;
    public int GetCoin;
    public int Atk;
    public float AtkDelay;

    public override void SetJsonData(string key, JObject info)
    {
        base.SetJsonData(key, info);

        unitNo = Int32.Parse(key);
        Name = info["Name"].Value<string>();
        GetCoin = info["GetCoin"].Value<int>();
        Atk = info["Atk"].Value<int>();
        AtkDelay = info["AtkDelay"].Value<float>();
    }
}
