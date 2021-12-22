using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using Define;

public class UserDataManager : Singleton<UserDataManager>
{
    public UserData userData = new UserData();

    public GameObject NameInput;

    string filepath;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        filepath = Application.persistentDataPath + "/UserData.json";
    }

    public void LoadUserData()
    {
        // Start Coroutine LoadData
        StartCoroutine(LoadData());
    }

    void ResetUserData()
    {
        //init UserData
        NameInput.SetActive(true);
        userData.Coin = 0;
        userData.CurrentVillagerCoin = 0;
        userData.VillagerDictionary.Clear();
        userData.VillagerDictionary.Add("NormalVillager", true);
        InitCurrentVillager();
        InitLevel();
        InitCropOpen();

        // Start Coroutine SaveData
        StartCoroutine(SaveData());

        // Start Coroutine LoadData
        StartCoroutine(LoadData());
    }

    public void InitCurrentVillager()
    {
        userData.CurrentVillager.Clear();
        userData.FarmingVillager.Clear();
        for (int i = 0; i < DataBaseManager.Instance.tdVillagerDict.Count; i++)
        {
            userData.CurrentVillager.Add(i + (int)VillagerEnum.빌, 0);
            userData.FarmingVillager.Add(i + (int)VillagerEnum.빌, 0);    
        }
        for (int j = 0; j < 6; j++)
        {
            if (j == 0) { userData.IsFarmOpen.Add(j, true); }
            else { userData.IsFarmOpen.Add(j, false); }
        }
    }

    void InitLevel()
    {
        userData.ShopLevel.Clear();
        foreach (var j in DataBaseManager.Instance.tdShopDict.Values)
        {
            userData.ShopLevel.Add(j.Name, 1);
        }
    }

    void InitCropOpen()
    {
        userData.IsCropOpen.Clear();
        foreach (var j in DataBaseManager.Instance.tdCropDict.Values)
        {
            userData.IsCropOpen.Add(j.Name, false);
        }
    }

    public IEnumerator LoadData()
    {
        StopCoroutine(SaveData());
        if (!File.Exists(filepath)) { ResetUserData(); yield return new WaitForSeconds(0.2f); }


        // Json 파일 불러오기
        string code = File.ReadAllText(filepath);
        // Json 암호화 해독
        byte[] bytes = System.Convert.FromBase64String(code);
        string jdata = System.Text.Encoding.UTF8.GetString(bytes);

        // Json 변환
        userData = JsonConvert.DeserializeObject<UserData>(jdata);
        yield return new WaitForSeconds(0.1f);
    }

    public IEnumerator SaveData()
    {
        string jdata = JsonConvert.SerializeObject(userData);
        // json 암호화 
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jdata);
        string code = System.Convert.ToBase64String(bytes);
        // Json 파일 저장
        File.WriteAllText(filepath, code);
        yield return new WaitForSeconds(0.1f);
    }

    public IEnumerator SaveDataDelay()
    {
        yield return new WaitForSecondsRealtime(1f);
        StartCoroutine(SaveData());
        StartCoroutine(SaveDataDelay());
    }
}
