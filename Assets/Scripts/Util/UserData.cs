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
    // ��� ��ȭ �ָӴ� ���� ��ȭ ��
    public int CurrentGoblinCoin;

    public Dictionary<string, bool> GoblinDictionary = new Dictionary<string, bool>();
    // �ʵ� ��� ��
    public Dictionary<int, int> CurrentGoblin = new Dictionary<int, int>(); // <"������ȣ", "����">
}
