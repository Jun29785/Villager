using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI Text_GoblinCoin;

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
    }

    private void UIText()
    {
        GoblinCoinText();
    }

    private void GoblinCoinText()
    {
        var gm = GameManager.Instance;
        Text_GoblinCoin.text = gm.goblinCoin.ToString() + "/" + gm.limitGoblinCoin.ToString();
    }

    public void ClickGoblinCoin()
    {
        if (GameManager.Instance.goblinCoin > 0)
        {
            // Spawn Goblin
            GoblinManager.Instance.SpawnGoblin();
            GameManager.Instance.goblinCoin -= 1;
        }
    }
}
