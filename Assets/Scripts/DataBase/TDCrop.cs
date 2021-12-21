using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDCrop : TableBase
{
    public int Key;
    public string Name;
    public int Cost;

    public override void SetJsonData(string key, JObject info)
    {
        base.SetJsonData(key, info);

        Key = int.Parse(key);
        Name = info["Name"].Value<string>();
        Cost = info["Cost"].Value<int>();
    }
}