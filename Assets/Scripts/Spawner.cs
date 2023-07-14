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
    [SerializeField] private Transform hitEffect;
    [SerializeField] private int hitEffectPoolCount;
    private Queue<GameObject> hitEffectPool = new Queue<GameObject>();
    //--------------------------------------
    private GameObject tempObject;
    //--------------------------------------

    private void Awake()
    {
        Instance = this;
        GenerateBulletPool();
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

    private void GenerateHitEffect()
    {
        for (int i = 0; i < hitEffectPoolCount; i++)
        {
            tempObject = Instantiate(prefabs.pfHitEffect, bulletPoolHolder);
            tempObject.SetActive(false);
            hitEffectPool.Enqueue(tempObject);
        }
    }

    //-------------------------------------------
    public GameObject GetBullet()
    {
        tempObject = BulletPool.Dequeue();
        tempObject.SetActive(true);
        tempObject.GetComponent<Bullet>().ResetBullet();
        BulletPool.Enqueue(tempObject);
        return tempObject;
    }

    public GameObject GetHitEffect()
    {
        tempObject = hitEffectPool.Dequeue();
        tempObject.SetActive(true);
        hitEffectPool.Enqueue(tempObject);
        return tempObject;
    }

}
