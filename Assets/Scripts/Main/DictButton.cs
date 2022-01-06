using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DictButton : MonoBehaviour, IPointerClickHandler
{
    [Header("Value")]
    public int Key;
    public string Name;
    public int GetCoin;
    public int CropTime;
    public int CropAmount;
    public int CombineCoin;
    public string Description;

    private TMPro.TextMeshProUGUI tx;

    private void OnEnable()
    {

        tx = transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameUIManager.Instance.OnClickDictButton(this);
    }

    public void SetButton(int key)
    {
        Debug.Log("key : " + key);
        this.Key = key;
        var VillagerDict = DataBaseManager.Instance.tdVillagerDict[this.Key];

        this.Name = VillagerDict.Name;
        this.GetCoin = VillagerDict.GetCoin;
        this.CropTime = VillagerDict.CropTime;
        this.CropAmount = VillagerDict.CropAmount;
        this.CombineCoin = VillagerDict.CombineCoin;
        this.Description = VillagerDict.Description;
        tx.text = Name;
    }
}
