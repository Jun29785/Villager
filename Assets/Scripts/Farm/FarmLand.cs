using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Numerics;

public class FarmLand : MonoBehaviour,IPointerClickHandler
{
    [Header("State")]
    private bool IsLock;
    public int FarmNum;
    private BigInteger Cost;
    public string cost;

    private void Start()
    {
        Cost = BigInteger.Parse(cost);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (UserDataManager.Instance.userData.IsFarmOpen[FarmNum])
            FarmUIManager.Instance.OnClickFarmLand();
    }

    void SetFarmLand()
    {
        
    }
}
