using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FarmManager : MonoBehaviour
{
   public static FarmManager Instance;

    FarmVillager CurrentSelect;

    DateTime Now;

    TimeSpan duration = new TimeSpan();
    
    private void Awake()
    {
        Instance = this;
    }

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

