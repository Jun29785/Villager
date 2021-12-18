using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButton : MonoBehaviour
{
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

    public void SetButton(int key)
    {
        this.Key = key;
        var ShopDict = DataBaseManager.Instance.tdShopDict[this.Key];

        this.Name = ShopDict.Name;
        this.Cost = ShopDict.Cost;
        this.Value = ShopDict.Value;
        this.Level = UserDataManager.Instance.userData.ShopLevel[this.Key.ToString()];
    }
}
