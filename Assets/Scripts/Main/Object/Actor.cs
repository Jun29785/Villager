using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;

public abstract class Actor : MonoBehaviour
{
    [Header("State")]
    public int UnitNo;
    public string Name;
    public BigInteger GetCoin;
    public BigInteger Atk;
    public float AtkDelay;

    public Animator anim;

    public abstract void SetData(int Key);
    public virtual void Awake()
    {

    }

    public virtual void Start()
    {
        anim = GetComponent<Animator>();

    }

    public virtual void Update()
    {
        if (!PlunderManager.Instance.isPlunder)
        {
            return;
        }
    }

    public virtual void FixedUpdate()
    {
        if (!PlunderManager.Instance.isPlunder)
        {
            return;
        }
    }
}
