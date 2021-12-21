using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System;

public class TDVillager : TableBase
{
    public int unitNo;
    public string Name;
    public int GetCoin;
    public int CropTime;
    public int CropAmount;
    public int CombineCoin;
    public string Description;

    public override void SetJsonData(string key, JObject info)
    {
        base.SetJsonData(key, info);

        unitNo = int.Parse(key);
        Name = info["Name"].Value<string>();
        GetCoin = info["GetCoin"].Value<int>();
        CropTime = info["CropTime"].Value<int>();
        CropAmount = info["CropAmount"].Value<int>();
        CombineCoin = info["CombineCoin"].Value<int>();
        Description = info["Description"].Value<string>();
    }
}
