using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private int GoblinCoin;
    public int goblinCoin
    {
        get
        {
            return GoblinCoin;
        }
        set
        {
            GoblinCoin = value;
        }
    }

    private int LimitGoblinCoin;
    public int limitGoblinCoin
    {
        get
        {
            return LimitGoblinCoin;
        }
        set
        {
            LimitGoblinCoin = value;
        }
    }

    private float GoblinCoinDelay;
    public float goblinCoinDelay
    {
        get
        {
            return GoblinCoinDelay;
        }
        set
        {
            GoblinCoinDelay = value;
        }
    }

    private int FieldGoblin;
    public int fieldGoblin
    {
        get
        {
            return FieldGoblin;
        }
        set
        {
            FieldGoblin = value;
        }
    }

    private int LimitFieldGoblin;
    public int limitFieldGoblin
    {
        get
        {
            return LimitFieldGoblin;
        }

        set
        {
            LimitFieldGoblin = value;
        }
    }

    public TitleController titleController;

    public Vector2 PointerPosition;

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
        limitGoblinCoin = 10;
        limitFieldGoblin = 10;
    }

    public void ApplicationSetting()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.orientation = ScreenOrientation.Portrait;
    }
}
