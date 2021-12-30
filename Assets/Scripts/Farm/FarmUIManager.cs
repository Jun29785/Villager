using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
using UnityEngine.UI;
using Define;
using System;

public class FarmUIManager : MonoBehaviour
{
    #region Variables
    public static FarmUIManager Instance;

    [Header("FarmManager")]
    public GameObject Fmanager;

    [Header("Text")]
    public GameObject Coin;
    private TMPro.TextMeshProUGUI Text_Coin;
    private TMPro.TextMeshProUGUI Text_Package; // 수확된 꾸러미

    [Header("Settings")]
    public GameObject Settings;

    [Header("Select")]
    public GameObject Select;
    private Transform SelectObjParent;
    public GameObject SelectObj;
    private TMPro.TextMeshProUGUI Text_Timer;
    private TMPro.TextMeshProUGUI Text_SelectedVillager;
    private TMPro.TextMeshProUGUI Text_SelectedCrop;
    private GameObject Selected;
    Growth grow;

    [Header("Variables")]
    BigInteger coin;
    string[] coinUnitArr = new string[] { "", "만", "억", "조", "경", "해", "자", "양", "가", "구", "간" };
    #endregion

    private void Awake()
    {
        Instance = this;
        grow = Growth.Wait;
    }

    private void Start()
    {
        #region FindObj
        Text_Coin = Coin.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>();
        SelectObjParent = Select.transform.GetChild(0).GetChild(3).GetChild(0);
        Text_Timer = Select.transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();
        Text_SelectedVillager = Select.transform.GetChild(0).GetChild(6).GetComponent<TMPro.TextMeshProUGUI>();
        Text_SelectedCrop = Select.transform.GetChild(0).GetChild(7).GetComponent<TMPro.TextMeshProUGUI>();
        Selected = Select.transform.GetChild(1).gameObject;
        #endregion
    }

    private void Update()
    {
        ShowText();
        coin = UserDataManager.Instance.userData.Coin;
    }

    public string GetCoinText(BigInteger value)
    {
        int placeN = 4;
        List<int> numList = new List<int>();
        int p = (int)Mathf.Pow(10, placeN);

        do
        {
            numList.Add((int)(value % p));
            value /= p;
        } while (value >= 1);
        string retStr = "";

        for (int i = 0; i < numList.Count; i++)
        {
            if (i > numList.Count - 3)
            {
                retStr = numList[i] + coinUnitArr[i] + " " +retStr;
            }
        }
        return retStr;
    }

    void ShowText()
    {
        CoinText();
        if (Select.activeSelf)
        {
            SelectText();
            TimerText();
        }
    }

    void CoinText()
    {
        Text_Coin.text = GetCoinText(coin);
    }

    #region Settings
    public void OnClickSettings()
    {
        Settings.SetActive(true);
    }

    public void ExitSettings()
    {
        Settings.SetActive(false);
    }
    #endregion

    #region Select
    public void OnClickFarmLand()
    {
        Select.SetActive(true);
        CreateVillagerSelect();
    }

    public void OnClickSelectButton()
    {
        var Key = Fmanager.GetComponent<FarmManager>().CurrentFarmNumber;
        var FM = Fmanager.GetComponent<FarmManager>();
        var selected = Selected.GetComponent<FarmSelected>();
        if (grow == Growth.Get)
        {
            Debug.Log("1");
            // 수확
            UserDataManager.Instance.userData.EndFarmingTime[Key] = "";
            UserDataManager.Instance.userData.SelectedCrop[Key] = 0;
            UserDataManager.Instance.userData.SelectedVillager[Key] = 0;
            selected.GetComponent<FarmSelected>().ResetChild();
        }
        else if (grow == Growth.Start && (FM.CurrentSelectVillager != null || FM.CurrentSelectCrop != null))
        {
            Debug.Log("2");
            FM.SetTime();
        }
        else
        {
            Debug.Log("3");
            // 알림 메세지 호출
        }
        if (FM.CurrentSelectVillager != null && FM.CurrentSelectCrop != null)
        {
        }
    }

    private void CreateVillagerSelect()
    {
        for (int i = 0; i < SelectObjParent.childCount; i++)
        {
            Destroy(SelectObjParent.GetChild(i).gameObject);
        }

        foreach (var i in UserDataManager.Instance.userData.CurrentVillager)
        {
            if (i.Value > 0)
            {
                for (int j = 0; j < i.Value; j++)
                {
                    GameObject Create = (GameObject)Instantiate(SelectObj);
                    Create.transform.parent = SelectObjParent;
                    Create.transform.localScale = new UnityEngine.Vector3(1, 1, 1);
                    Create.GetComponent<FarmSelect>().SetButton(i.Key);
                }
            }
        }
    }

    private void CreateCropSelect()
    {
        for (int i = 0; i < SelectObjParent.childCount; i++)
        {
            Destroy(SelectObjParent.GetChild(i).gameObject);
        }

        foreach (var i in UserDataManager.Instance.userData.IsCropOpen)
        {
            if (i.Value)
            {
                GameObject Create = (GameObject)Instantiate(SelectObj);
                Create.transform.parent = SelectObjParent;
                Create.transform.localScale = new UnityEngine.Vector3(1, 1, 1);
                Create.GetComponent<FarmSelect>().SetButton(i.Key);
            }
        }
    }

    public void OnClickVillagerButton()
    {
        CreateVillagerSelect();
    }

    public void OnClickCropButton()
    {
        CreateCropSelect(); 
    }

    private void TimerText()
    {
        int FNum = Fmanager.GetComponent<FarmManager>().CurrentFarmNumber;
        if (UserDataManager.Instance.userData.EndFarmingTime[FNum] != "")
        {
            DateTime edt = Convert.ToDateTime(UserDataManager.Instance.userData.EndFarmingTime[FNum]);
            DateTime now = Fmanager.GetComponent<FarmManager>().Now;
            TimeSpan i = edt.Subtract(now);
            Text_Timer.text = GetTimerText(i);
        }
        else
        {
            Text_Timer.text = GetTimerText();
        }
    }

    string GetTimerText()
    {
        string Timer = "";
        var se = Selected.GetComponent<FarmSelected>();
        if (se.Crop == null || se.Villager == null)
        {
            Timer += "선택 대기 중";
            grow = Growth.Wait;
        }
        else
        {
            Timer += "시작하기";
            grow = Growth.Start;
        }
        return Timer;
    }

    string GetTimerText(TimeSpan ts)
    {
        string Timer = "";

        if (ts.Hours > 0)
        {
            Timer += ts.Hours.ToString("00") + ":";
            Timer += ts.Minutes.ToString("00") + ":";
            Timer += ts.Seconds.ToString("00");
        }
        else if (ts.Minutes>0)
        {
            Timer += ts.Minutes.ToString("00") + ":";
            Timer += ts.Seconds.ToString("00");
        }
        else if (ts.Seconds > 0)
        {
            Timer += ts.Seconds.ToString("00");
        }
        else
        {
            Timer = "수확하기";
            grow = Growth.Get;
            return Timer;
        }
        grow = Growth.Growth;
        return Timer;
    }

    private void SelectText()
    {
        int Key = Fmanager.GetComponent<FarmManager>().CurrentFarmNumber;
        var Sd = Selected.GetComponent<FarmSelected>();
        if (Sd.Crop != null)
        {
            Text_SelectedCrop.text = "작물 : " + DataBaseManager.Instance.tdCropDict[Sd.Crop.Key].Name;
        }
        else
        {
            Text_SelectedCrop.text = "작물 : 선택 안됨";
        }
        if (Sd.Villager != null)
        {
            Text_SelectedVillager.text = "주민 : " + DataBaseManager.Instance.tdVillagerDict[Sd.Villager.Key].Name;
        }
        else 
        { 
            Text_SelectedVillager.text = "주민 : 선택 안됨"; 
        }
    }

    public void Farm(int Key,FarmSelect fs)
    {
        if (Key > 10000 && Key < 20000)
        {
            if (Selected.GetComponent<FarmSelected>().Villager != null) return;
            UserDataManager.Instance.userData.SelectedVillager[Fmanager.GetComponent<FarmManager>().CurrentFarmNumber] = Key;
            UserDataManager.Instance.userData.CurrentVillager[Key] -= 1;
            Debug.Log("move");
            fs.transform.parent = Selected.transform;
            fs.gameObject.SetActive(false);
            
        }
        else if (Key > 30000 && Key < 40000)
        {
            if (Selected.GetComponent<FarmSelected>().Crop != null) return;
            UserDataManager.Instance.userData.SelectedCrop[Fmanager.GetComponent<FarmManager>().CurrentFarmNumber] = Key;
            fs.transform.parent = Selected.transform;
        }
    }

    public void FarmLand(int FarmNum)
    {
        if (UserDataManager.Instance.userData.IsFarmOpen[FarmNum])
        {
            OnClickFarmLand();
            Fmanager.GetComponent<FarmManager>().CurrentFarmNumber = FarmNum;
            var crop = UserDataManager.Instance.userData.SelectedCrop[FarmNum];
            if (crop > 30000)
            {
                GameObject i = (GameObject)Instantiate(SelectObj);                
                i.GetComponent<FarmSelect>().SetButton(crop);
                i.transform.parent = Selected.transform;
                Selected.GetComponent<FarmSelected>().Crop = i.GetComponent<FarmSelect>();
                i.SetActive(false);
            }
            var vil = UserDataManager.Instance.userData.SelectedVillager[FarmNum];
            if (vil > 10000)
            {
                GameObject i = (GameObject)Instantiate(SelectObj);
                i.GetComponent<FarmSelect>().SetButton(vil);
                i.transform.parent = Selected.transform;
                Selected.GetComponent<FarmSelected>().Villager = i.GetComponent<FarmSelect>();
                i.SetActive(false);
            }
        }
    }

    public void ExitSelect()
    {
        var FM = Fmanager.GetComponent<FarmManager>().CurrentFarmNumber;
        if (UserDataManager.Instance.userData.EndFarmingTime[FM] == "")
        {
            // Selected child Remove
            Selected.GetComponent<FarmSelected>().ResetChild();
            Debug.Log("Reset Select");
            UserDataManager.Instance.userData.SelectedCrop[FM] = 0;
            UserDataManager.Instance.userData.SelectedVillager[FM] = 0;
        }
        Select.SetActive(false);
    }
    #endregion

    public void OnClickGoVillage()
    {
        SceneController.LoadScene("MainScene");
    }
}