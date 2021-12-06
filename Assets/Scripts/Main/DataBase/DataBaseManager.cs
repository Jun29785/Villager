using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;


public class DataBaseManager : Singleton<DataBaseManager>
{
    public Dictionary<int, TDGoblin> tdGoblinDict = new Dictionary<int, TDGoblin>();

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public void LoadTable()
    {
        Debug.Log("테이블 로딩 시작");
        LoadGoblinTable();
    }

    void LoadGoblinTable()
    {
        TextAsset jsonText = Resources.Load<TextAsset>("DataTable/Goblin_Json");

        tdGoblinDict.Clear();
        
        JObject parseObj = new JObject();

        parseObj = JObject.Parse(jsonText.text);

        foreach(KeyValuePair<string,JToken> pair in parseObj)
        {
            TDGoblin tdGoblin = new TDGoblin();

            tdGoblin.SetJsonData(pair.Key, pair.Value.ToObject<JObject>());
            tdGoblinDict.Add(tdGoblin.unitNo, tdGoblin);
        }

        Debug.Log("고블린 테이블 완료");
    }
}
