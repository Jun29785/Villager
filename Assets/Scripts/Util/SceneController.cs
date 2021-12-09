using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneController : MonoBehaviour
{
    [SerializeReference]
    static string nextScene;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public static void LoadScene(string SceneName)
    {
        nextScene = SceneName;
        UserDataManager.Instance.SaveData();
        SceneManager.LoadScene(SceneName);
    }
}
