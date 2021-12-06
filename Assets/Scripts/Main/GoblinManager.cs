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
        var obj = Objectpool.GetGoblinObject((int)goblinEnum.단검고블린, RandomSpawnPos());
    }

    private Vector2 RandomSpawnPos()
    {
        // Random Vector2
        Vector2 pos = new Vector2(Random.Range(-2.5f,2.5f),Random.Range(-4.65f,0));
        return pos;
    }
}
