using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;

public class GameManager : Singleton<GameManager>
{
    #region Variables

    private int VillagerCoin;
    public int villagerCoin
    {
        get
        {
            return VillagerCoin;
        }
        set
        {
            VillagerCoin = value;
        }
    }

    private int LimitVillagerCoin;
    public int limitVillagerCoin
    {
        get
        {
            return LimitVillagerCoin;
        }
        set
        {
            LimitVillagerCoin = value;
        }
    }

    private float VillagerCoinDelay;
    public float villagerCoinDelay
    {
        get
        {
            return VillagerCoinDelay;
        }
        set
        {
            VillagerCoinDelay = value;
        }
    }

    private int FieldVillager;
    public int fieldVillager
    {
        get
        {
            return FieldVillager;
        }
        set
        {
            FieldVillager = value;
        }
    }

    private int LimitFieldVillager;
    public int limitFieldvillager
    {
        get
        {
            return LimitFieldVillager;
        }

        set
        {
            LimitFieldVillager = value;
        }
    }



    private BigInteger Coin;
    public BigInteger coin
    {
        get
        {
            return Coin;
        }
        set
        {
            Coin = value;
        }
    }

    public TitleController titleController;

    public UnityEngine.Vector2 PointerPosition;

    #endregion

    protected override void Awake()
    {
        base.Awake();

        // Init Game
        Init();
        DontDestroyOnLoad(this);
    }

    private void FixedUpdate()
    {
        PointerPosition = Input.mousePosition;
    }

    private void Start()
    {
        titleController.Initialize();
    }

    private void Init()
    {
        limitVillagerCoin = 10;
        limitFieldvillager = 10;
    }

    public void ApplicationSetting()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.orientation = ScreenOrientation.Portrait;
    }
}
