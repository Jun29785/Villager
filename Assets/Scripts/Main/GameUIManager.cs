using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Numerics;

public class GameUIManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI Text_Coin;
    public TMPro.TextMeshProUGUI Text_VillagerCoin;
    public TMPro.TextMeshProUGUI Text_FieldVillager;
    public GameObject ObjectPool;

    bool canGetVillagerCoin = true;

    string[] coinUnitArr = new string[] { "", "만", "억", "조", "경", "해", "자", "양", "가", "구", "간" };
    BigInteger coin;
    
    IEnumerator GetVillagerCoin(float delay)
    {
        yield return new WaitForSeconds(delay);

        var gm = GameManager.Instance;

        if (gm.villagerCoin < gm.limitVillagerCoin)
        {
            gm.villagerCoin += 1;
            if (gm.villagerCoin < gm.limitVillagerCoin)
            {
                StartCoroutine(GetVillagerCoin(gm.villagerCoinDelay));
            }
        }
    }

    private void Awake()
    {
        GameManager.Instance.villagerCoinDelay = 1;
        Initialize();
        StartCoroutine(UserDataManager.Instance.SaveDataDelay());
    }

    private void Update()
    {
        UIText();
        GetFieldVillagerCount();
        if (canGetVillagerCoin)
        {
            canGetVillagerCoin = false;
            StartCoroutine(GetVillagerCoin(GameManager.Instance.villagerCoinDelay));
        }
        coin = GameManager.Instance.coin;
        UserDataManager.Instance.userData.CurrentVillagerCoin = GameManager.Instance.villagerCoin;
    }

    void Initialize()
    {
        var gm = GameManager.Instance;
        var um = UserDataManager.Instance.userData;
        gm.villagerCoin = um.CurrentVillagerCoin;
        gm.coin = um.Coin;
    }

    private void UIText()
    {
        CoinText();
        VillagerCoinText();
        FieldVillagerText();
    }

    private void CoinText()
    {
        Text_Coin.text = GetCoinText();
    }

    private void VillagerCoinText()
    {
        var gm = GameManager.Instance;
        Text_VillagerCoin.text = gm.villagerCoin.ToString() + "/" + gm.limitVillagerCoin.ToString();
    }
     
    public void ClickVillagerCoin()
    {
        var gm = GameManager.Instance;
        if (gm.villagerCoin > 0 &&gm.fieldVillager<gm.limitVillagerCoin)
        {
            // Spawn Villager
            VillagerManager.Instance.SpawnVillager();
            GameManager.Instance.villagerCoin -= 1;
            if (!canGetVillagerCoin && GameManager.Instance.villagerCoin == GameManager.Instance.limitVillagerCoin-1)
            {
                canGetVillagerCoin = true;
            }
        }
        StartCoroutine(UserDataManager.Instance.SaveDataDelay());
    }
    
    private void GetFieldVillagerCount()
    {
        int count = 0;
        foreach (Transform child in ObjectPool.transform)
        {
            if (child.gameObject.activeSelf)
                count++;
        }
        GameManager.Instance.fieldVillager = count;
    }

    private void FieldVillagerText()
    {
        var gm = GameManager.Instance;
        Text_FieldVillager.text = gm.fieldVillager.ToString() + "/" + gm.limitVillagerCoin.ToString();
    }

    private string GetCoinText()
    {
        int placeN = 4;
        BigInteger value = coin;
        List<int> numList = new List<int>();
        int p = (int)Mathf.Pow(10, placeN);

        do
        {
            numList.Add((int)(value % p));
            value /= p;
        } while (value >= 1);
        string retStr = "";

        for (int i = 0; i < numList.Count; i++)
        {
            if (i > numList.Count - 3)
            {
                retStr = numList[i] + coinUnitArr[i]+ " " + retStr;
            }
        }
        return retStr;
    }
}
