using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    [Header("Value")]
    [SerializeField]
    public int Key;
    [SerializeField]
    public string Name;
    [SerializeField]
    public int Cost;
    [SerializeField]
    public int Value;
    [SerializeField]
    public int Level;

    [Header("Text")]
    TMPro.TextMeshProUGUI Text_Name;
    TMPro.TextMeshProUGUI Text_Level;

    public void SetButton(int key)
    {
        Debug.Log("key : " + key);
        this.Key = key;
        var ShopDict = DataBaseManager.Instance.tdShopDict[this.Key];

        this.Name = ShopDict.Name;
        this.Cost = ShopDict.Cost;
        this.Value = ShopDict.Value;
        this.Level = UserDataManager.Instance.userData.ShopLevel[this.Name.ToString()];
    }

    private void Start()
    {
        Text_Name = transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>();
        Text_Level = transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>();
    }

    private void Update()
    {
        ShowText();
    }

    void ShowText()
    {
        Text_Name.text = Name;
        Text_Level.text = Level.ToString() + " ·¹º§";
    }

}
