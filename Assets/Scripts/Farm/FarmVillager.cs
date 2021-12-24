using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FarmVillager : MonoBehaviour,IPointerClickHandler
{
    public int Key;
    public int CropTime;
    public int CropAmount;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click " + name);
        // Color Change & Show Infomation
    }

    public void SetButton(int key)
    {
        name = key.ToString();
        Key = key;
        CropTime = DataBaseManager.Instance.tdVillagerDict[key].CropTime;
        CropAmount = DataBaseManager.Instance.tdVillagerDict[key].CropAmount;

    }
}
