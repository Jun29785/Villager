using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    [Header("Value")]
    public int Key;
    public string Name;
    public int Cost;
    public int Value;
    public int Level;

    [Header("Text")]
    TMPro.TextMeshProUGUI Text_Name;
    TMPro.TextMeshProUGUI Text_Level;

    public void SetButton(int key)
    {
        this.Key = key;
        var ShopDict = DataBaseManager.Instance.tdShopDict[this.Key];

        this.Name = ShopDict.Name;
        this.Value = ShopDict.Value;
        this.Level = UserDataManager.Instance.userData.ShopLevel[this.Name.ToString()];
        this.Cost = DataBaseManager.Instance.tdShopDict[key].Cost;
        for (int i = 1; i < Level; i++)
        {
            Cost += (Cost / 2);
        }
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
