using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObjects/Prefabs")]
public class PrefabsSO : ScriptableObject
{
    public PlacedObjectTypeSO PowerPlant;
    public PlacedObjectTypeSO Barracks;
    public PlacedObjectTypeSO Base;
    public Transform[] Soldiers;
    [Space]
    public GameObject pfBullet;
    public GameObject pfHitEffect;
    public GameObject pfWorldText;
}
