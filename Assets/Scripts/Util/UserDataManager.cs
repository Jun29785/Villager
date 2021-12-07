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
        Debug.Log("filepath : " + filepath);
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
        userData.CurrentGoblinCoin = 0;
        userData.GoblinDictionary.Clear();
        userData.GoblinDictionary.Add("NormalGoblin", true);
        userData.CurrentGoblin.Clear();

        // Start Coroutine SaveData
        StartCoroutine(SaveData());

        // Start Coroutine LoadData
        StartCoroutine(LoadData());
    }

    public IEnumerator LoadData()
    {
        if (!File.Exists(filepath)) { ResetUserData(); yield return new WaitForSeconds(0.2f); }

        // Json 파일 불러오기
        string code = File.ReadAllText(filepath);
        Debug.Log("jdata"+code);
        // Json 암호화 해독
        byte[] bytes = System.Convert.FromBase64String(code);
        string jdata = System.Text.Encoding.UTF8.GetString(bytes);
        
        // Json 변환
        userData = JsonConvert.DeserializeObject<UserData>(jdata);
        yield return new WaitForSeconds(0.1f);
        CheckUserDataValue();
    }

    public IEnumerator SaveData()
    {
        string jdata = JsonConvert.SerializeObject(userData);
        Debug.Log("data"+jdata);
        // json 암호화 
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jdata);
        string code = System.Convert.ToBase64String(bytes);

        // Json 파일 저장
        File.WriteAllText(filepath, code);
        yield return new WaitForSeconds(0.1f);
        CheckUserDataValue();
    }

    void CheckUserDataValue()
    {
        Debug.Log("UserName : " + userData.UserName + " Coin : " + userData.Coin + " GoblinCoin : " + userData.CurrentGoblinCoin +
            " GoblinDictionary : " + userData.GoblinDictionary.Keys + " CurrentGoblin : " + userData.CurrentGoblin.Keys);
    }
}
