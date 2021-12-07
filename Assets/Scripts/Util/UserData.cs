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
    public int CurrentGoblinCoin;

    public Dictionary<string, bool> GoblinDictionary = new Dictionary<string, bool>();
    // 필드 고블린 수
    public Dictionary<string, int> CurrentGoblin = new Dictionary<string, int>(); // <"고블린 이름", "개수">
}
