using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    int CoinAmount;

    UserData user = UserDataManager.Instance.userData;

    private void Update()
    {
        CoinAmount = 1000000;
    }

    public void TradeCoin()
    {
        Debug.Log("coin");
        if (user.Package > 0)
        {
            UserDataManager.Instance.userData.Package -= 1;
            user.Coin += CoinAmount;
            FarmUIManager.Instance.updatepkgTx();
        }    
    }

    public void TradeRose()
    {
        Debug.Log("rose");
        if(user.Package > 0)
        {
            UserDataManager.Instance.userData.Package -= 1;
            user.Flower += 1;
            FarmUIManager.Instance.updatepkgTx();
        }
    }
}
