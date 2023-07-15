using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObjects/Prefabs")]
public class PrefabsSO : ScriptableObject
{
    public Transform PowerPlant;
    public Transform Barracks;
    public Transform Base;
    [Space]
    public Sprite PowerPlantSprite;
    public Sprite BarracksSprite;
    [Space]
    public Transform[] Soldiers;
    [Space]
    public GameObject pfBullet;
    public GameObject pfHitEffect;
    public GameObject pfWorldText;
}
