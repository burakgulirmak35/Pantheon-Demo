using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private SettingsSO settings;
    [SerializeField] private PrefabsSO prefabs;
    public static Spawner Instance { get; private set; }
    //--------------------------------------
    [SerializeField] private Transform bulletPoolHolder;
    [SerializeField] private int bulletPoolCount;
    private Queue<GameObject> BulletPool = new Queue<GameObject>();
    //--------------------------------------
    private GameObject tempObject;
    //--------------------------------------

    private void Awake()
    {
        Instance = this;
    }



    private void GenerateBulletPool()
    {
        for (int i = 0; i < bulletPoolCount; i++)
        {
            tempObject = Instantiate(prefabs.pfBullet, bulletPoolHolder);
            tempObject.SetActive(false);
            BulletPool.Enqueue(tempObject);
        }
    }

    //-------------------------------------------
    public GameObject PoolGetBullet()
    {
        tempObject = BulletPool.Dequeue();
        tempObject.SetActive(true);
        BulletPool.Enqueue(tempObject);
        return tempObject;
    }

}
