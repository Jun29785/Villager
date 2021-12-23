using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;

public class FarmUIManager : MonoBehaviour
{
    public static FarmUIManager Instance;

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

    [Header("Variables")]
    BigInteger coin;
    string[] coinUnitArr = new string[] { "", "만", "억", "조", "경", "해", "자", "양", "가", "구", "간" };

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Text_Coin = Coin.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>();
        SelectObjParent = Select.transform.GetChild(0).GetChild(3).GetChild(0);
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
                retStr = numList[i] + coinUnitArr[i] + " " + retStr;
            }
        }
        return retStr;
    }

    void ShowText()
    {
        CoinText();
    }

    void CoinText()
    {

        Text_Coin.text = UserDataManager.Instance.userData.Coin.ToString();
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

    public void ExitSelect()
    {
        Select.SetActive(false);
    }
    #endregion

    public void OnClickGoVillage()
    {
        SceneController.LoadScene("MainScene");
    }
}