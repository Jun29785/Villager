using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Abstract parent class inhibit
public class Villager : Actor, IDragHandler, IPointerDownHandler, IDropHandler
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

    private void OnEnable()
    {
        StartCoroutine(Move());
        StartCoroutine(EarnCoin());
    }

    private void OnDisable()
    {
        StopCoroutine(Move());
        StopCoroutine(EarnCoin());
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
        Pointer = GameManager.Instance.PointerPosition;
    }

    public override void SetData(int Key)
    {
        var dict = DataBaseManager.Instance.tdVillagerDict[Key];
        UnitNo = dict.unitNo;
        Name = dict.Name;
        GetCoin = dict.GetCoin;
        CropTime = dict.CropTime;
        CropAmount = dict.CropAmount;
        CombineCoin = dict.CombineCoin;
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
        yield return new WaitForFixedUpdate();
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

    // Combine Villager To Next Step
    private void OnCollisionStay2D(Collision2D collision)
    {
        Combine(collision);
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

    void Combine(Collision2D collision)
    {
        if (CanCombine && collision.gameObject.CompareTag("Villager") && this.UnitNo == collision.gameObject.GetComponent<Villager>().UnitNo)
        {
            CanCombine = false;
            // Get Position (other)
            Vector2 pos = collision.gameObject.GetComponent<RectTransform>().anchoredPosition;
            // Remove this and other
            Objectpool.ReturnVillager(this);
            Objectpool.ReturnVillager(collision.gameObject.GetComponent<Villager>());
            // Create Next Villager
            Villager obj = Objectpool.GetVillagerObject(UnitNo + 1, pos);
            obj.GetComponent<RectTransform>().anchoredPosition = pos;
            obj.transform.localScale = new Vector3(1, 1, 1);
            obj.CanCombine = false;
            GameManager.Instance.AddCoin(CombineCoin);
        }
    }

    IEnumerator EarnCoin()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.AddCoin(GetCoin);
        StartCoroutine(EarnCoin());
    }
}
