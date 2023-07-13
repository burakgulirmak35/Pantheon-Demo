using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private SettingsSO settings;
    [SerializeField] private PrefabsSO prefabs;
    public static PoolManager Instance { get; private set; }
    //--------------------------------------
    private Transform uiPoolHolder;
    private Queue<GameObject> moneyUiPool = new Queue<GameObject>();
    private Queue<GameObject> damageUiPool = new Queue<GameObject>();
    //--------------------------------------
    [SerializeField] private Transform bulletPoolHolder;
    private Queue<GameObject> rifleBulletPool = new Queue<GameObject>();
    //--------------------------------------
    private GameObject tempObject;
    //--------------------------------------

    private void Awake()
    {
        Instance = this;
    }

    private void GeneratePool(GameObject prefab, int count, Queue<GameObject> pool, Transform holder)
    {
        for (int i = 0; i < count; i++)
        {
            tempObject = Instantiate(prefab, holder);
            tempObject.SetActive(false);
            pool.Enqueue(tempObject);
        }
    }

    //-------------------------------------------
    public GameObject PoolGetRifleBullet()
    {
        tempObject = rifleBulletPool.Dequeue();
        tempObject.SetActive(true);
        rifleBulletPool.Enqueue(tempObject);
        return tempObject;
    }
}
