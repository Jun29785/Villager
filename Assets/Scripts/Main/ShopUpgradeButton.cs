using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopUpgradeButton : MonoBehaviour, IPointerClickHandler
{
    ShopButton Shop;
    TMPro.TextMeshProUGUI Text_UpgradeCost;
    int GrowthMaxLevel = 6;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (UserDataManager.Instance.userData.Coin >= Shop.Cost)
        {
            if (Shop.Key == 20005 && Shop.Level < GrowthMaxLevel)
            {
                UserDataManager.Instance.userData.IsCropOpen[Shop.Level + 30000] = true;
                UserDataManager.Instance.userData.Coin -= Shop.Cost;
                UserDataManager.Instance.userData.ShopLevel[Shop.Name] += 1;
            }
            else if (Shop.Key == 20004 && Shop.Level < 10)
            {
                UserDataManager.Instance.userData.Coin -= Shop.Cost;
                UserDataManager.Instance.userData.ShopLevel[Shop.Name] += 1;
            }
            else if (Shop.Key != 20005 && Shop.Key != 20004) 
            {
                UserDataManager.Instance.userData.Coin -= Shop.Cost;
                UserDataManager.Instance.userData.ShopLevel[Shop.Name] += 1;
            }
            Shop.SetButton(Shop.Key);
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

    void ShowText()
    {
        switch (Shop.Key)
        {
            case 20004:
                if (Shop.Level >= 10)
                {
                    Text_UpgradeCost.text = "최고 레벨";
                }
                else
                {
                    Text_UpgradeCost.text = GameUIManager.Instance.GetCoinText(Shop.Cost);
                }
                break;
            case 20005:
                if (Shop.Level >= GrowthMaxLevel)
                {
                    Text_UpgradeCost.text = "최고 레벨";
                }
                else
                {
                    Text_UpgradeCost.text = GameUIManager.Instance.GetCoinText(Shop.Cost);
                }
                break;
            default:
                Text_UpgradeCost.text = GameUIManager.Instance.GetCoinText(Shop.Cost);
                break;
        }
    }
}
