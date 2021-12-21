using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmUIManager : MonoBehaviour
{
    public FarmUIManager Instance;

    [Header("Text")]
    public GameObject Coin;
    private TMPro.TextMeshProUGUI Text_Coin;
    private TMPro.TextMeshProUGUI Text_Package; // 수확된 꾸러미

    [Header("Settings")]
    public GameObject Settings;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Text_Coin = Coin.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>();
    }

    void ShowText()
    {

    }

    void CoinText()
    {
        Text_Coin.text = UserDataManager.Instance.userData.Coin.ToString();
    }

    #region Settings
    public void OnClickSettings()
    {
        Settings.SetActive(true);
    }

    public void ExitSettings()
    {
        Settings.SetActive(false);
    }
    #endregion
}
