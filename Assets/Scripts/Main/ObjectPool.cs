using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private GameObject[] GoblinPrefabs;

    MultiMap<int, Goblin> GoblinMap = new MultiMap<int, Goblin>();

    private void Awake()
    {
        Instance = this;
        Initialize(GameManager.Instance.limitGoblinCoin);
    }

    private void Initialize(int count)
    {
        CreateNewGoblins();
    }

    /// <summary>
    /// 새로운 객체 만들기
    /// </summary>
    private void CreateNewGoblins()
    {
        foreach (GameObject goblin in GoblinPrefabs)
        {
            var newObj = Instantiate(goblin).GetComponent<Goblin>();
            newObj.transform.parent = Instance.transform;
            newObj.gameObject.SetActive(false);
            GoblinMap.Add(newObj.UnitNo, newObj);
        }
    }

    public static Goblin GetGoblinObject(int GoblinNo, Vector2 pos)
    {
        if (Instance.GoblinMap[GoblinNo].Count > 0)
        {
            var obj = Instance.GoblinMap.Removeit(GoblinNo);
            Debug.Log("1 Obj Pos : " + obj.transform.position);
            obj.transform.position = pos;
            Debug.Log("2 Obj Pos : " + obj.transform.position);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            Instance.CreateNewGoblins();
            var newobj = Instance.GoblinMap.Removeit(GoblinNo);
            newobj.gameObject.SetActive(true);
            return newobj;
        }
    }

    /// <summary>
    /// 사용하지 않는 객체 비활성화
    /// </summary>
    /// <param name="obj">비활성화 시킬 객체</param>
    public static void ReturnGoblin(Goblin obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.GoblinMap.Add(obj.UnitNo, obj);
    }
}
