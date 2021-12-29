using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
using System;

[Serializable]
public class UserData
{
    public string UserName;
    public BigInteger Coin;
    public int Package;
    public bool IsOpenFarmLand;

    // 주민 시계 수
    public int CurrentVillagerCoin;

    public Dictionary<string, bool> VillagerDictionary = new Dictionary<string, bool>();
    // 필드 주민 수
    public Dictionary<int, int> CurrentVillager = new Dictionary<int, int>(); // <"고유번호", "개수">
    public Dictionary<string, int> ShopLevel = new Dictionary<string, int>();

    public Dictionary<int, bool> IsCropOpen = new Dictionary<int, bool>();

    // 농작지 오픈
    public Dictionary<int, bool> IsFarmOpen = new Dictionary<int, bool>();
    public Dictionary<int, string> EndFarmingTime = new Dictionary<int, string>();
    public Dictionary<int, int> FarmPackageAmount = new Dictionary<int, int>();
    public Dictionary<int, int> SelectedVillager = new Dictionary<int, int>();
    public Dictionary<int, int> SelectedCrop = new Dictionary<int, int>();
}
