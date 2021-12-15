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
        obj.transform.position = pos;
        obj.transform.localScale = new Vector3(1, 1, 1);
    }

    private Vector2 RandomSpawnPos()
    {
        // Random Vector2
        Vector2 pos = new Vector2(Random.Range(120f,1320f),Random.Range(120f,2840f));
        Debug.Log(pos);
        return pos;
    }
}
