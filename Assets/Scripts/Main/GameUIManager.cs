using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Numerics;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance;

    [Header("Text")]
    public TMPro.TextMeshProUGUI Text_Coin;
    public TMPro.TextMeshProUGUI Text_VillagerCoin;
    public TMPro.TextMeshProUGUI Text_FieldVillager;

    [Header("ObjectPool")]
    public GameObject ObjectPool;

    [Header("Shop")]
    public GameObject ShopPanel;
    public GameObject ShopObj;
    private Transform ShopObjParent;

    [Header("Dictionary")]
    public GameObject DictPanel;
    public GameObject DictObj;
    private Transform DictObjParent;
    public GameObject DictInfo;
    private TMPro.TextMeshProUGUI DictName;
    private TMPro.TextMeshProUGUI DictCoin;
    private TMPro.TextMeshProUGUI DictDescription;

    bool canGetVillagerCoin = true;

    string[] coinUnitArr = new string[] { "", "만", "억", "조", "경", "해", "자", "양", "가", "구", "간" };
    BigInteger coin;
    
    IEnumerator GetVillagerCoin(float delay)
    {
        yield return new WaitForSeconds(delay);

        var gm = GameManager.Instance;

        if (gm.villagerCoin < gm.limitVillagerCoin)
        {
            gm.villagerCoin += 1;
            if (gm.villagerCoin < gm.limitVillagerCoin)
            {
                StartCoroutine(GetVillagerCoin(gm.villagerCoinDelay));
            }
        }
    }

    private void Awake()
    {
        Instance = this;
        GameManager.Instance.villagerCoinDelay = 1;
        Initialize();
        StartCoroutine(UserDataManager.Instance.SaveDataDelay());
    }

    private void Start()
    {
        #region Find Object
        ShopObjParent = ShopPanel.transform.GetChild(0).GetChild(0);
        DictObjParent = DictPanel.transform.GetChild(1).GetChild(0);
        DictName = DictInfo.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>();
        DictCoin = DictInfo.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>();
        DictDescription = DictInfo.transform.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>();
        #endregion
    }

    private void Update()
    {
        UIText();
        GetFieldVillagerCount();
        if (canGetVillagerCoin)
        {
            canGetVillagerCoin = false;
            StartCoroutine(GetVillagerCoin(GameManager.Instance.villagerCoinDelay));
        }
        coin = UserDataManager.Instance.userData.Coin;
        UserDataManager.Instance.userData.CurrentVillagerCoin = GameManager.Instance.villagerCoin;
    }

    void Initialize()
    {
        var gm = GameManager.Instance;
        var um = UserDataManager.Instance.userData;
        gm.villagerCoin = um.CurrentVillagerCoin;
        gm.coin = um.Coin;
    }

    private void UIText()
    {
        CoinText();
        VillagerCoinText();
        FieldVillagerText();
    }

    private void CoinText()
    {
        Text_Coin.text = GetCoinText();
    }

    private void VillagerCoinText()
    {
        var gm = GameManager.Instance;
        Text_VillagerCoin.text = gm.villagerCoin.ToString() + "/" + gm.limitVillagerCoin.ToString();
    }
     
    public void ClickVillagerCoin()
    {
        var gm = GameManager.Instance;
        if (gm.villagerCoin > 0 &&gm.fieldVillager<gm.limitVillagerCoin)
        {
            // Spawn Villager
            VillagerManager.Instance.SpawnVillager();
            GameManager.Instance.villagerCoin -= 1;
            if (!canGetVillagerCoin && GameManager.Instance.villagerCoin == GameManager.Instance.limitVillagerCoin-1)
            {
                canGetVillagerCoin = true;
            }
        }
        StartCoroutine(UserDataManager.Instance.SaveDataDelay());
    }
    
    private void GetFieldVillagerCount()
    {
        int count = 0;
        foreach (Transform child in ObjectPool.transform)
        {
            if (child.gameObject.activeSelf)
                count++;
        }
        GameManager.Instance.fieldVillager = count;
    }

    private void FieldVillagerText()
    {
        var gm = GameManager.Instance;
        Text_FieldVillager.text = gm.fieldVillager.ToString() + "/" + gm.limitVillagerCoin.ToString();
    }

    private string GetCoinText()
    {
        int placeN = 4;
        BigInteger value = coin;
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
                retStr = numList[i] + coinUnitArr[i]+ " " + retStr;
            }
        }
        return retStr;
    }

    private void CheckFieldVillager()
    {
        UserDataManager.Instance.InitCurrentVillager();
        for (int i = 0; i < ObjectPool.transform.childCount; i++)
        {
            if (ObjectPool.transform.GetChild(i).gameObject.activeSelf)
            {
                UserDataManager.Instance.userData.CurrentVillager.Add(
                    ObjectPool.transform.GetChild(i).GetComponent<Actor>().UnitNo, UserDataManager.Instance.userData.CurrentVillager[
                        ObjectPool.transform.GetChild(i).GetComponent<Actor>().UnitNo]+1);
            }
        }
    }
    #region Shop
    public void OnClickShop()
    {
        ShopPanel.SetActive(true);
        CreateShop();
    }

    private void CreateShop()
    {
        for (int i = 0; i<ShopObjParent.childCount; i++)
        {
            Destroy(ShopObjParent.GetChild(i).gameObject);
        }

        foreach (var i in DataBaseManager.Instance.tdShopDict.Values)
        {
            Debug.Log("in");
            GameObject Create = (GameObject)Instantiate(ShopObj);
            Create.transform.parent = ShopObjParent;
            Create.transform.localScale = new UnityEngine.Vector3(1, 1, 1);
            Create.GetComponent<ShopButton>().SetButton(i.Key);
        }
    }

    public void ExitShop()
    {
        ShopPanel.SetActive(false);
    }
    #endregion

    #region Dictionary
    public void OnClickDictionary()
    {
        DictPanel.SetActive(true);
        CreateDictionary();
    }

    private void CreateDictionary()
    {
        for (int i = 0; i < DictObjParent.childCount; i++)
        {
            Destroy(DictObjParent.GetChild(i).gameObject);
        }

        foreach (var i in DataBaseManager.Instance.tdVillagerDict.Values)
        {
            Debug.Log("in");
            GameObject Create = (GameObject)Instantiate(DictObj);
            Create.transform.parent = DictObjParent;
            Create.transform.localScale = new UnityEngine.Vector3(1, 1, 1);
            Debug.Log("UnitNo : " + i.unitNo);
            Create.GetComponent<DictButton>().SetButton(i.unitNo);
        }
    }

    public void ExitDictionary()
    {
        DictInfo.SetActive(false);
        DictPanel.SetActive(false);
    }

    public void OnClickDictButton(DictButton dictButton)
    {
        DictName.text = "이름 : " + dictButton.Name;
        DictCoin.text = "초당 코인 : " + dictButton.GetCoin;
        DictDescription.text = "\"" + dictButton.Description + "\"";
        DictInfo.SetActive(true);
    }
    #endregion
}
