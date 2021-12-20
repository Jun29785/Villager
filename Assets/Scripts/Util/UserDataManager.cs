using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

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
        userData.CurrentVillager.Clear();
        InitLevel();

        // Start Coroutine SaveData
        StartCoroutine(SaveData());

        // Start Coroutine LoadData
        StartCoroutine(LoadData());
    }

    void InitLevel()
    {
        userData.ShopLevel.Clear();
        foreach (var j in DataBaseManager.Instance.tdShopDict.Values)
        {
            userData.ShopLevel.Add(j.Name, 1);
        }
        
    }

    public IEnumerator LoadData()
    {
        StopCoroutine(SaveData());
        if (!File.Exists(filepath)) { ResetUserData(); yield return new WaitForSeconds(0.2f); }

        // Json ���� �ҷ�����
        string code = File.ReadAllText(filepath);
        // Json ��ȣȭ �ص�
        byte[] bytes = System.Convert.FromBase64String(code);
        string jdata = System.Text.Encoding.UTF8.GetString(bytes);
        
        // Json ��ȯ
        userData = JsonConvert.DeserializeObject<UserData>(jdata);
        yield return new WaitForSeconds(0.1f);
    }

    public IEnumerator SaveData()
    {
        string jdata = JsonConvert.SerializeObject(userData);
        // json ��ȣȭ 
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jdata);
        string code = System.Convert.ToBase64String(bytes);

        // Json ���� ����
        File.WriteAllText(filepath, code);
        yield return new WaitForSeconds(0.1f);
    }

    public IEnumerator SaveDataDelay()
    {
        yield return new WaitForSecondsRealtime(3f);
        Debug.Log("Save User Data");
        StartCoroutine(SaveData());
        StartCoroutine(SaveDataDelay());
    }
}
