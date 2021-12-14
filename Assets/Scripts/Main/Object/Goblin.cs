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
        
    }

    public override void Start()
    {
        base.Start();
        StartCoroutine(Move());
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
        return new Vector2(vector.x + (float)Random.Range(-0.7f, 0.7f), vector.y + (float)Random.Range(-0.7f, 0.7f));
    }

    IEnumerator Move()
    {

        yield return new WaitForSeconds(6f);
        if (isClicked) { }
        else
        {
            while (true)
            {
                Vector2 vector = RandomPosition(this.gameObject.transform.position);
                if (vector.x > 2.5f || vector.x < -2.5 || vector.y > 4.5 || vector.y < -4.5) continue;
                // Exception Out Range Check
                this.gameObject.transform.position = vector;
                break;
            }
        }
        StartCoroutine(Move());
    }
}
