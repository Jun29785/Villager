using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class UserDataManager : Singleton<UserDataManager>
{
    public UserData userData = new UserData();
    string filepath;

    private void Start()
    {
        filepath = Application.persistentDataPath + "/UserData.json";
    }
    public void LoadUserData()
    {
        StartCoroutine(LoadData());
    }

    void ResetUserData()
    {
        //init UserData

    }

    IEnumerator LoadData()
    {
        if (!File.Exists(filepath)) { ResetUserData(); yield return new WaitForSeconds(0.2f); }

        string jdata = File.ReadAllText(filepath);
        Debug.Log("jdata"+jdata);
        userData = JsonConvert.DeserializeObject<UserData>(jdata);
        yield return new WaitForSeconds(0.2f);
    }

    IEnumerator SaveData()
    {
        string data = JsonConvert.SerializeObject(userData);
        Debug.Log("data"+data);
        File.WriteAllText(filepath, data);
        yield return new WaitForSeconds(0.2f);
    }
}
