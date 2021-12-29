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
        if (UserDataManager.Instance.userData.Coin >= Shop.Cost)
        {
            UserDataManager.Instance.userData.Coin -= Shop.Cost;
            UserDataManager.Instance.userData.ShopLevel[Shop.Name] += 1;
            Shop.SetButton(Shop.Key);
            if (Shop.Key == 20005)
            {
                UserDataManager.Instance.userData.IsCropOpen[Shop.Level + 30000] = true;
            }
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
        Text_UpgradeCost.text = GameUIManager.Instance.GetCoinText(Shop.Cost);
    }
}
