using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Numerics;
using System;

public class FarmLand : MonoBehaviour,IPointerClickHandler
{
    [Header("State")]
    private bool IsLock;
    public int FarmNum;
    public BigInteger Cost;
    public string cost;
    public TimeSpan Duration;
    private void Start()
    {
        Cost = BigInteger.Parse(cost);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        FarmUIManager.Instance.Fmanager.GetComponent<FarmManager>().FL = this;
        FarmUIManager.Instance.FarmLand(FarmNum);
    }
}
