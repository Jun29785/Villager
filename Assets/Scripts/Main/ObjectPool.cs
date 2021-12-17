using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

public class MultiMap<TKey, TValue>
{
    private readonly Dictionary<TKey, IList<TValue>> storage;

    public MultiMap()
    {
        storage = new Dictionary<TKey, IList<TValue>>();
    }

    public void Add(TKey key, TValue value)
    {
        if (!storage.ContainsKey(key)) storage.Add(key, new List<TValue>());
        storage[key].Add(value);
    }

    public IEnumerable<TKey> Keys
    {
        get { return storage.Keys; }
    }

    public bool ContainsKey(TKey key)
    {
        return storage.ContainsKey(key);
    }

    public IList<TValue> this[TKey key]
    {
        get
        {
            if (!storage.ContainsKey(key))
                throw new KeyNotFoundException(
                    string.Format(
                        "The given key {0} was not found in the collection.", key));
            return storage[key];
        }
    }

    public TValue Removeit(TKey unit)
    {
        var obj = storage[unit][0];
        storage[unit].RemoveAt(0);

        return obj;
    }
}

public class Objectpool : MonoBehaviour
{
    public static Objectpool Instance;

    [Header("Prefab Object")]
    [SerializeField]
    private GameObject[] VillagerPrefabs;

    MultiMap<int, Villager> VillagerMap = new MultiMap<int, Villager>();

    private void Awake()
    {
        Instance = this;
        Initialize(GameManager.Instance.limitVillagerCoin);
    }

    private void Initialize(int count)
    {
        CreateNewVillagers((int)VillagerEnum.��);
        CreateNewVillagers((int)VillagerEnum.��Ƽ��);
        CreateNewVillagers((int)VillagerEnum.�˷���);
        CreateNewVillagers((int)VillagerEnum.�ȳ�);
    }

    /// <summary>
    /// ���ο� ��ü �����
    /// </summary>
    private void CreateNewVillagers(int VillagerNo)
    {
        GameObject Villager = VillagerPrefabs[VillagerNo - (int)VillagerEnum.��]; 
        var newObj = Instantiate(Villager).GetComponent<Villager>();
        newObj.transform.parent = Instance.transform;
        newObj.gameObject.SetActive(false);
        VillagerMap.Add(newObj.UnitNo, newObj);
        
    }

    public static Villager GetVillagerObject(int VillagerNo, Vector2 pos)
    {
        if (Instance.VillagerMap[VillagerNo].Count > 0)
        {
            var obj = Instance.VillagerMap.Removeit(VillagerNo);
            obj.gameObject.SetActive(true);
            obj.GetComponent<RectTransform>().anchoredPosition = pos;
            return obj;
        }
        else
        {
            Instance.CreateNewVillagers(VillagerNo);
            var newobj = Instance.VillagerMap.Removeit(VillagerNo);
            newobj.gameObject.SetActive(true);
            return newobj;
        }
    }

    /// <summary>
    /// ������� �ʴ� ��ü ��Ȱ��ȭ
    /// </summary>
    /// <param name="obj">��Ȱ��ȭ ��ų ��ü</param>
    public static void ReturnVillager(Villager obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.VillagerMap.Add(obj.UnitNo, obj);
    }
}
