using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class TDShop : TableBase
{
    public int unitNo;
    public string Name;
    public int Value;

    public override void SetJsonData(string key, JObject info)
    {
        base.SetJsonData(key, info);

        unitNo = int.Parse(key);
        Name = info["Name"].Value<string>();
        Value = info["Value"].Value<int>();
    }
}
