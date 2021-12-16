using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Numerics;

public class GameUIManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI Text_Coin;
    public TMPro.TextMeshProUGUI Text_GoblinCoin;
    public TMPro.TextMeshProUGUI Text_FieldGoblin;
    public GameObject ObjectPool;

    bool canGetGoblinCoin = true;

    string[] coinUnitArr = new string[] { "", "만", "억", "조", "경", "해", "자", "양", "가", "구", "간" };
    BigInteger coin;
    
    IEnumerator GetGoblinCoin(float delay)
    {
        yield return new WaitForSeconds(delay);

        var gm = GameManager.Instance;

        if (gm.goblinCoin < gm.limitGoblinCoin)
        {
            gm.goblinCoin += 1;
            if (gm.goblinCoin < gm.limitGoblinCoin)
            {
                StartCoroutine(GetGoblinCoin(gm.goblinCoinDelay));
            }
        }
    }

    private void Awake()
    {
        GameManager.Instance.goblinCoinDelay = 1;
        Initialize();
        StartCoroutine(UserDataManager.Instance.SaveDataDelay());
    }

    private void Update()
    {
        UIText();
        GetFieldGoblinCount();
        if (canGetGoblinCoin)
        {
            canGetGoblinCoin = false;
            StartCoroutine(GetGoblinCoin(GameManager.Instance.goblinCoinDelay));
        }
        coin = GameManager.Instance.coin;
        UserDataManager.Instance.userData.CurrentGoblinCoin = GameManager.Instance.goblinCoin;
    }

    void Initialize()
    {
        var gm = GameManager.Instance;
        var um = UserDataManager.Instance.userData;
        gm.goblinCoin = um.CurrentGoblinCoin;
        gm.coin = um.Coin;
    }

    private void UIText()
    {
        CoinText();
        GoblinCoinText();
        FieldGoblinText();
    }

    private void CoinText()
    {
        Text_Coin.text = GetCoinText();
    }

    private void GoblinCoinText()
    {
        var gm = GameManager.Instance;
        Text_GoblinCoin.text = gm.goblinCoin.ToString() + "/" + gm.limitGoblinCoin.ToString();
    }
     
    public void ClickGoblinCoin()
    {
        var gm = GameManager.Instance;
        if (gm.goblinCoin > 0 &&gm.fieldGoblin<gm.limitFieldGoblin)
        {
            // Spawn Goblin
            GoblinManager.Instance.SpawnGoblin();
            GameManager.Instance.goblinCoin -= 1;
            if (!canGetGoblinCoin && GameManager.Instance.goblinCoin == GameManager.Instance.limitGoblinCoin-1)
            {
                canGetGoblinCoin = true;
            }
        }
        StartCoroutine(UserDataManager.Instance.SaveDataDelay());
    }
    
    private void GetFieldGoblinCount()
    {
        int count = 0;
        foreach (Transform child in ObjectPool.transform)
        {
            if (child.gameObject.activeSelf)
                count++;
        }
        GameManager.Instance.fieldGoblin = count;
    }

    private void FieldGoblinText()
    {
        var gm = GameManager.Instance;
        Text_FieldGoblin.text = gm.fieldGoblin.ToString() + "/" + gm.limitFieldGoblin.ToString();
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
            if (i > numList.Count - 2)
            {
                retStr = numList[i] + coinUnitArr[i] + retStr;
            }
        }
        return retStr;
    }
}
