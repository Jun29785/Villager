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
    // 고블린 금화 주머니 안의 금화 수
    public int CurrentVillagerCoin;

    public Dictionary<string, bool> VillagerDictionary = new Dictionary<string, bool>();
    // 필드 고블린 수
    public Dictionary<int, int> CurrentVillager = new Dictionary<int, int>(); // <"고유번호", "개수">
    public Dictionary<string, int> ShopLevel = new Dictionary<string, int>();
}
