using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;

public abstract class Actor : MonoBehaviour
{
    [Header("State")]
    public int UnitNo;
    public string Name;
    public int GetCoin;
    public int CropTime;
    public int CropAmount;
    public int CombineCoin;

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
        
    }

    public virtual void FixedUpdate()
    {
        
    }
}
