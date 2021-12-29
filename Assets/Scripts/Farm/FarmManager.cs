using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FarmManager : MonoBehaviour
{
    public FarmSelect CurrentSelectVillager = null;
    public FarmSelect CurrentSelectCrop = null;
    public DateTime Now;
    public FarmSelected Selected;

    public int CurrentFarmNumber;
    private double Duration;
    private int Amount;

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        GetNow();
    }

    private void Update()
    {
        GetSelected();
        GetNow();
    }

    void GetNow()
    {
        Now = DateTime.Now;
    }

    public void SetTime()
    {
        Debug.Log("Set Time");
        Debug.Log(CurrentSelectVillager.Name);
        Duration += CurrentSelectVillager.Duration;
        Debug.Log(CurrentSelectCrop.Name);
        Duration += CurrentSelectCrop.Duration;
        DateTime dateTime = Now.AddSeconds(Duration);
        string endTime = dateTime.ToString("yyyy/MM/dd HH:mm:ss");
        Debug.Log("EndTime : " + endTime);
        Amount += CurrentSelectCrop.Amount;
        Amount += CurrentSelectVillager.Amount;

        UserDataManager.Instance.userData.FarmPackageAmount[CurrentFarmNumber] = Amount;
        // 유저데이터에 끝나는 시간 저장 (string)
        UserDataManager.Instance.userData.EndFarmingTime[CurrentFarmNumber] = endTime;
        //UserDataManager.Instance.userData.EndFarmingTime[CurrentFarmNumber] = ;

        StartCoroutine(UserDataManager.Instance.SaveData());
    }

    private void GetSelected()
    {
       if (Selected.Crop != null && Selected.Villager != null)
        {
            CurrentSelectCrop = Selected.Crop;
            CurrentSelectVillager = Selected.Villager;
        }
    }

    void Init()
    {
        CurrentSelectCrop = null;
        CurrentSelectVillager = null;
        Duration = 0;
        Amount = 0;
    }
}

