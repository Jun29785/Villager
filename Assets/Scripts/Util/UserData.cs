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
    public int CurrentGoblinCoin;

    public Dictionary<string, int> GoblinDictionary = new Dictionary<string, int>();
    public Dictionary<string, int> CurrentGoblin = new Dictionary<string, int>(); // <"고블린 이름", "개수">

}
