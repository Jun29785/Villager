using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Abstract parent class inhibit
public class Goblin : Actor
{
    bool isClicked;
    public override void Awake()
    {
        base.Awake();
        // Move
        StartCoroutine(Move());
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();

    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void SetData(int Key)
    {
        var dict = DataBaseManager.Instance.tdGoblinDict[Key];
        UnitNo = dict.unitNo;
        Name = dict.Name;
        GetCoin = dict.GetCoin;
        Atk = dict.Atk;
        AtkDelay = dict.AtkDelay;
    }

    Vector2 RandomPosition(Vector2 vector)
    {
        return new Vector2(vector.x + (float)Random.Range(-10, 10) / 10f, vector.y + (float)Random.Range(-10, 10) / 10f);
    }

    IEnumerator Move()
    {
        yield return new WaitForSeconds(6f);
        if (isClicked) { }
        else
        {
            Vector2 vector = RandomPosition(this.gameObject.transform.position);
            // Exception Out Range Check
        }
        StartCoroutine(Move());
    }
}
