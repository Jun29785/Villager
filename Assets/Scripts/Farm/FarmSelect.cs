using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FarmSelect : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    public int Key;
    public string Name;
    public int Amount;
    public int Duration;
    public int Cost;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked Select Button : " + Name);
        FarmUIManager.Instance.Farm(this.Key, this);
        // color change
    }

    public void SetButton(int Key)
    {
        this.Key = Key;
        if (Key > 10000 && Key < 20000)
        {
            Name = DataBaseManager.Instance.tdVillagerDict[Key].Name;
            Amount = DataBaseManager.Instance.tdVillagerDict[Key].CropAmount;
            Duration = DataBaseManager.Instance.tdVillagerDict[Key].CropTime;
        }
        else if (Key > 30000 && Key < 40000)
        {
            Name = DataBaseManager.Instance.tdCropDict[Key].Name;
            Cost = DataBaseManager.Instance.tdCropDict[Key].Cost;
            Amount = DataBaseManager.Instance.tdCropDict[Key].Amount;
            Duration = DataBaseManager.Instance.tdCropDict[Key].GrowthDuration;
        }
    }
}
