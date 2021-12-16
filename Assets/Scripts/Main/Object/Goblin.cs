using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Abstract parent class inhibit
public class Goblin : Actor, IDragHandler, IPointerDownHandler, IDropHandler
{
    bool isClicked;
    float MoveSpeed;
    Vector2 Pointer;
    float DragSpeed;
    bool CanCombine;
    public override void Awake()
    {
        base.Awake();
        MoveSpeed = 70f;
        CanCombine = false;
        DragSpeed = 10000000f;
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
        return new Vector2(vector.x + (float)Random.Range(-200f, 200f), vector.y + (float)Random.Range(-200f, 200f));
    }

    IEnumerator Move()
    {
        if (isClicked) { }
        else
        {
            while (true)
            {
                var ancPos = GetComponent<RectTransform>().anchoredPosition;
                Vector2 vector = new Vector2();
                vector = RandomPosition(ancPos);
                if (vector.x > 260f || vector.x < -260f || vector.y > 590f || vector.y < -590f) continue;
                for (int i = 0; i < 70; i++)
                {
                    this.gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.MoveTowards(this.gameObject.GetComponent<RectTransform>().anchoredPosition, vector, MoveSpeed * Time.deltaTime);
                    yield return new WaitForFixedUpdate();
                }
                break;
            }
        }
        yield return new WaitForSeconds(4.5f);
        StartCoroutine(Move());
    }

    IEnumerator WaitSecond()
    {
        yield return new WaitForSeconds(0.7f);
        CanCombine = false;
        StartCoroutine(Move());
    }
    public void OnDrag(PointerEventData eventData)
    {
        Drag();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PointerDown();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Drop();
    }

    // Combine Goblin To Next Step
    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("CollisionEnter" + "\nCanCombine : " + CanCombine);
        if (CanCombine && collision.gameObject.CompareTag("Goblin"))
        {
            Debug.Log("StartCombine");
            CanCombine = false;
            // Get Position (other)
            Vector2 pos = collision.gameObject.GetComponent<RectTransform>().anchoredPosition;
            Debug.Log("pos : " + pos);
            // Remove this and other
            Objectpool.ReturnGoblin(this);
            Objectpool.ReturnGoblin(collision.gameObject.GetComponent<Goblin>());
            // Create Next Goblin
            Goblin obj = Objectpool.GetGoblinObject(UnitNo + 1, pos);
            obj.GetComponent<RectTransform>().anchoredPosition = pos;
            obj.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    // PointerDown
    public void PointerDown()
    {
        isClicked = true;

    }

    // PointerUp
    public void Drop()
    {
        isClicked = false;
        CanCombine = true;
        StartCoroutine(WaitSecond());
    }

    // Drag
    public void Drag()
    {
        transform.position = Vector2.MoveTowards(transform.position, Pointer, DragSpeed * Time.deltaTime);
    }
}
