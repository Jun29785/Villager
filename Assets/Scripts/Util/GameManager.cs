using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public TitleController titleController; 

    protected override void Awake()
    {
        base.Awake();

        // Init Game
        Init();
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        titleController.Initialize();
    }

    private void Init()
    {
        limitGoblinCoin = 10;
    }

    public void ApplicationSetting()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.orientation = ScreenOrientation.Portrait;
    }
}
