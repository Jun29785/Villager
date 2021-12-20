using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopUpgradeButton : MonoBehaviour, IPointerClickHandler
{
    ShopButton Shop;
    TMPro.TextMeshProUGUI Text_UpgradeCost;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnClicked");
        if (GameManager.Instance.coin >= Shop.Cost)
        {
            Debug.Log("buy");
            UserDataManager.Instance.userData.ShopLevel[Shop.Name] += 1;
            Shop.SetButton(Shop.Key);
            Debug.Log("Level : " + Shop.Level);
        }
    }

    private void Start()
    {
        Shop = this.transform.parent.GetComponent<ShopButton>();
        Text_UpgradeCost = transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();
    }

    private void Update()
    {
        ShowText();
    }

    void  ShowText()
    {
        Text_UpgradeCost.text = Shop.Cost.ToString();
    }
}
