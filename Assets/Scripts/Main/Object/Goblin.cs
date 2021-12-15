using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Abstract parent class inhibit
public class Goblin : Actor
{
    bool isClicked;
    float MoveSpeed;
    Vector2 Pointer;
    float DragSpeed = 1000000f;
    public override void Awake()
    {
        base.Awake();
        MoveSpeed = 1.5f;
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
        Pointer = GameManager.Instance.PointerPosition;
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
        return new Vector2(vector.x + (float)Random.Range(-100f, 100f), vector.y + (float)Random.Range(-100f, 100f));
    }

    IEnumerator Move()
    {

        if (isClicked) { }
        else
        {
            while (true)
            {
                Vector2 vector = new Vector2();
                vector = RandomPosition(transform.position);
                if (vector.x > 1320f || vector.x < 120f || vector.y > 2840f || vector.y < 120f) continue;
                for (int i = 0; i < 1000; i++)
                {
                    
                    transform.position = Vector2.MoveTowards(transform.position, vector, MoveSpeed * Time.deltaTime * MoveSpeed *MoveSpeed*MoveSpeed*MoveSpeed*MoveSpeed*MoveSpeed*MoveSpeed*MoveSpeed);
                    yield return new WaitForFixedUpdate();                
                }
                break;
            }
        }
        yield return new WaitForSeconds(4.5f);
        StartCoroutine(Move());
    }

    // PointerDown
    public void PointerDown()
    {
        Debug.Log("down");
        isClicked = true;
    }

    // Drag
    public void Drag()
    {
        Debug.Log("Drag");
        transform.position = Vector2.MoveTowards(transform.position, Pointer, DragSpeed * Time.deltaTime);
    }

    public void Enter()
    {
        Debug.Log("Enter");
    }
}
