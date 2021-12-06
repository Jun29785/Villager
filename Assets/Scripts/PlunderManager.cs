using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlunderManager : Singleton<PlunderManager>
{
    public bool isPlunder;

    private void Awake()
    {
        isPlunder = false;
    }
}
