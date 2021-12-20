using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public Dictionary<string, int> ShopLevel = new Dictionary<string, int>();

    private void OnEnable()
    {
        InitializeShop();
    }

    public void InitializeShop()
    {
        ShopLevel.Clear();
        for (int i = 0; i <transform.childCount; i++)
        {
            var child = transform.GetChild(i).GetComponent<ShopButton>();
            ShopLevel.Add(child.Name, child.Level);
        }
    }
}
