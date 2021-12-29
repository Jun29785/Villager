using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class temp : MonoBehaviour
{
    DateTime Now;
    string td;
    DateTime tp;
    TimeSpan ts;

    private void Start()
    {
        GetNow();
    }

    public void convertdatetime()
    {
        tp = Convert.ToDateTime(td);
        Debug.Log("tp : " + tp);
    }

    public void getTd()
    {
        td = Now.ToString("yyyy/MM/dd HH:mm:ss");
        Debug.Log("td : " + td);
    }

    public void GetNow()
    {
        Now = DateTime.Now;
        Debug.Log("now : " + Now);      
    }

    public void Getspan()
    {
        ts = Now.Subtract(tp);
        Debug.Log("ts : " + ts);
    }
}
