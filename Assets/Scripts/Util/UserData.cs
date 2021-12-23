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
    // �ֹ� �ð� ��
    public int CurrentVillagerCoin;

    public Dictionary<string, bool> VillagerDictionary = new Dictionary<string, bool>();
    // �ʵ� �ֹ� ��
    public Dictionary<int, int> CurrentVillager = new Dictionary<int, int>(); // <"������ȣ", "����">
    public Dictionary<string, int> ShopLevel = new Dictionary<string, int>();

    public Dictionary<string, bool> IsCropOpen = new Dictionary<string, bool>();

    // ������ ����
    public Dictionary<int, bool> IsFarmOpen = new Dictionary<int, bool>();
    public Dictionary<int, int> FarmingVillager = new Dictionary<int, int>();
    public Dictionary<int, System.DateTime> FarmingTime = new Dictionary<int, DateTime>();
}
