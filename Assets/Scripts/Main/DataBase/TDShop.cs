using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class TDShop : TableBase
{
    public int Key;
    public string Name;
    public int Cost;
    public int Value;

    public override void SetJsonData(string key, JObject info)
    {
        base.SetJsonData(key, info);

        Key = int.Parse(key);
        Name = info["Name"].Value<string>();
        Cost = info["Cost"].Value<int>();
        Value = info["Value"].Value<int>();
    }
}
