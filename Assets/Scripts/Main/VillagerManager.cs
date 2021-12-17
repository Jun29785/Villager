using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

public class VillagerManager : Singleton<VillagerManager>
{
    void Start()
    {
        
    }

    public void SpawnVillager()
    {
        var pos = RandomSpawnPos();
        Villager obj = Objectpool.GetVillagerObject((int)VillagerEnum.ºô, pos);
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
