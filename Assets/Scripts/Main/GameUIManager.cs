using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI Text_GoblinCoin;
    public TMPro.TextMeshProUGUI Text_FieldGoblin;
    public GameObject ObjectPool;

    bool canGetGoblinCoin=true;
    IEnumerator GetGoblinCoin(float delay)
    {
        yield return new WaitForSeconds(delay);

        var gm = GameManager.Instance;

        if (gm.goblinCoin < gm.limitGoblinCoin)
        {
            gm.goblinCoin += 1;
        }
        StartCoroutine(GetGoblinCoin(gm.goblinCoinDelay));
    }

    private void Awake()
    {
        GameManager.Instance.goblinCoinDelay = 1;
        StartCoroutine(GetGoblinCoin(GameManager.Instance.goblinCoinDelay));
    }

    private void Update()
    {
        UIText();
        GetFieldGoblinCount();
        if (GameManager.Instance.goblinCoin > GameManager.Instance.limitGoblinCoin - 1 && canGetGoblinCoin)
        {
            StopCoroutine(GetGoblinCoin(GameManager.Instance.goblinCoinDelay));
            canGetGoblinCoin = false;
        }
        else
        {
            StartCoroutine(GetGoblinCoin(GameManager.Instance.goblinCoinDelay));
        }
    }

    private void UIText()
    {
        GoblinCoinText();
        FieldGoblinText();
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
            canGetGoblinCoin = true;
        }
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
}
