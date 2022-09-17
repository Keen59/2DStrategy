using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance { get; private set; }
    public List<PoolItems> items;
    public List<GameObject> PooledItems;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    public void GeneratePool()
    {
        PooledItems = new List<GameObject>();
        foreach (PoolItems item in items)
        {
            for (int i = 0; i < item.ItemAmount; i++)
            {
                var obj = Instantiate(item.prefab);
                obj.SetActive(false);
                PooledItems.Add(obj);
            }
        }
    }

    public GameObject Get(string tag)
    {
        for (int i = 0; i < PooledItems.Count; i++)
        {
            if (!PooledItems[i].activeInHierarchy && PooledItems[i].tag == tag)
            {
                return PooledItems[i];
            }
        }
        return null;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
[System.Serializable]
public class PoolItems
{
    public GameObject prefab;
    public int ItemAmount;
}