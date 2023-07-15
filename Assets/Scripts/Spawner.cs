using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spawner : MonoBehaviour
{
    [SerializeField] private SettingsSO settings;
    [SerializeField] private PrefabsSO prefabs;
    [Space]
    [SerializeField] private Transform PoolHolder;
    public static Spawner Instance { get; private set; }
    //--------------------------------------
    [SerializeField] private int bulletPoolCount;
    private Queue<GameObject> BulletPool = new Queue<GameObject>();
    //--------------------------------------
    [SerializeField] private int hitEffectPoolCount;
    private Queue<GameObject> hitEffectPool = new Queue<GameObject>();
    //--------------------------------------
    private int worldTextCount;
    private Queue<GameObject> worldTextPool = new Queue<GameObject>();
    //--------------------------------------
    private GameObject tempObject;
    //--------------------------------------

    private void Awake()
    {
        Instance = this;
        GenerateBulletPool();
        GenerateWorldTextPool();
    }

    private void GenerateWorldTextPool()
    {
        for (int i = 0; i < bulletPoolCount; i++)
        {
            tempObject = Instantiate(prefabs.pfWorldText, PoolHolder);
            tempObject.SetActive(false);
            worldTextPool.Enqueue(tempObject);
        }
    }

    private void GenerateBulletPool()
    {
        for (int i = 0; i < bulletPoolCount; i++)
        {
            tempObject = Instantiate(prefabs.pfBullet, PoolHolder);
            tempObject.SetActive(false);
            BulletPool.Enqueue(tempObject);
        }
    }

    private void GenerateHitEffect()
    {
        for (int i = 0; i < hitEffectPoolCount; i++)
        {
            tempObject = Instantiate(prefabs.pfHitEffect, PoolHolder);
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

    public GameObject GetWorldText()
    {
        tempObject = worldTextPool.Dequeue();
        tempObject.SetActive(true);
        worldTextPool.Enqueue(tempObject);
        return tempObject;
    }
    //-------------------------------------------

    public void CreateWorldTextPopup(string text, Vector3 localPosition)
    {
        tempObject = GetWorldText();
        StartCoroutine(DisableObject(tempObject, 1f));
        tempObject.transform.position = localPosition;
        tempObject.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
    }

    private IEnumerator DisableObject(GameObject go, float time)
    {
        yield return new WaitForSeconds(time);
        go.SetActive(false);
    }

}
