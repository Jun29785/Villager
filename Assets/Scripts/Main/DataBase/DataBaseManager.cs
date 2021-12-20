using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;


public class DataBaseManager : Singleton<DataBaseManager>
{
    public Dictionary<int, TDVillager> tdVillagerDict = new Dictionary<int, TDVillager>();

    public Dictionary<int, TDShop> tdShopDict = new Dictionary<int, TDShop>();

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public void LoadTable()
    {
        Debug.Log("테이블 로딩 시작");
        LoadVillagerTable();
        LoadShopTable();
    }

    void LoadVillagerTable()
    {
        TextAsset jsonText = Resources.Load<TextAsset>("DataTable/Villager_Json");

        tdVillagerDict.Clear();
        
        JObject parseObj = new JObject();

        parseObj = JObject.Parse(jsonText.text);

        foreach(KeyValuePair<string,JToken> pair in parseObj)
        {
            TDVillager tdVillager = new TDVillager();

            tdVillager.SetJsonData(pair.Key, pair.Value.ToObject<JObject>());
            tdVillagerDict.Add(tdVillager.unitNo, tdVillager);
        }
        Debug.Log(tdVillagerDict.Count);
        Debug.Log("주민 테이블 완료");
    }

    void LoadShopTable()
    {
        TextAsset jsonText = Resources.Load<TextAsset>("DataTable/Shop_Json");

        tdShopDict.Clear();

        JObject parseObj = new JObject();

        parseObj = JObject.Parse(jsonText.text);

        foreach(KeyValuePair<string,JToken> pair in parseObj)
        {
            TDShop tdShop = new TDShop();

            tdShop.SetJsonData(pair.Key, pair.Value.ToObject<JObject>());
            tdShopDict.Add(tdShop.Key, tdShop);
        }
        Debug.Log("상점 테이블 완료");
    }
}
