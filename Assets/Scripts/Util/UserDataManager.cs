using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System;
using Define;

public class UserDataManager : Singleton<UserDataManager>
{
    public UserData     userData = new UserData();

    public GameObject   NameInput;

    string              filepath;

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
        userData.Coin = 100000000000000000;
        userData.Package = 0;
        userData.IsOpenFarmLand = false;
        userData.CurrentVillagerCoin = 0;
        userData.VillagerDictionary.Clear();
        InitCurrentVillager();
        Init();
        InitCropOpen();

        // Start Coroutine SaveData
        StartCoroutine(SaveData());

        // Start Coroutine LoadData
        StartCoroutine(LoadData());
    }

    public void InitCurrentVillager()
    {
        userData.CurrentVillager.Clear();
        for (int i = 0; i < DataBaseManager.Instance.tdVillagerDict.Count; i++)
        {
            userData.CurrentVillager.Add(i + (int)VillagerEnum.빌, 0); 
        }
    }

    void Init()
    {
        userData.ShopLevel.Clear();
        userData.EndFarmingTime.Clear();
        foreach (var j in DataBaseManager.Instance.tdShopDict.Values)
        {
            userData.ShopLevel.Add(j.Name, 1);
        }
        for (int j = 0; j < 6; j++)
        {
            if (j == 0)
            {
                userData.IsFarmOpen.Add(j, true);
            }
            else
            {
                userData.IsFarmOpen.Add(j, false);
            }
            userData.EndFarmingTime.Add(j, "");
            userData.FarmPackageAmount.Add(j, 0);
            userData.SelectedCrop.Add(j, 0);
            userData.SelectedVillager.Add(j, 0);
        }
    }

    void InitCropOpen()
    {
        userData.IsCropOpen.Clear();
        foreach (var j in DataBaseManager.Instance.tdCropDict.Values)
        {
            if (j.Key == 30001)
            {
                userData.IsCropOpen.Add(j.Key, true);
                continue;
            }
            userData.IsCropOpen.Add(j.Key, false);
        }
    }

    public IEnumerator LoadData()
    {
        Debug.Log("Save");
        StopCoroutine(SaveData());
        if (!File.Exists(filepath)) { ResetUserData(); yield return new WaitForSeconds(0.2f); }


        // Json 파일 불러오기
        string code = File.ReadAllText(filepath);
        // Json 암호화 해독
        byte[] bytes = System.Convert.FromBase64String(code);
        string jdata = System.Text.Encoding.UTF8.GetString(bytes);
        Debug.Log("UserData : \n"+jdata);
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
