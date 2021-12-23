using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FarmaingManager : MonoBehaviour
{
    DateTime Now;

    private void Start()
    {
        GetNow();
    }

    void GetNow()
    {
        Now = DateTime.Now;
        Debug.Log("Now : " + Now.ToString("dd-hh-mm-ss"));
    }
}
