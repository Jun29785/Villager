using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

public class GoblinManager : Singleton<GoblinManager>
{
    void Start()
    {
        
    }

    public void SpawnGoblin()
    {
        var pos = RandomSpawnPos();
        Goblin obj = Objectpool.GetGoblinObject((int)goblinEnum.단검고블린, pos);
        obj.GetComponent<RectTransform>().anchoredPosition = pos;
        obj.transform.localScale = new Vector3(1, 1, 1);
    }

    private Vector2 RandomSpawnPos()
    {
        // Random Vector2
        Vector2 pos = new Vector2(Random.Range(-260f,260f),Random.Range(-590f,590f));
        return pos;
    }
}
