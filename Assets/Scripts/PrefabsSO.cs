using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObjects/Prefabs")]
public class PrefabsSO : ScriptableObject
{
    public PlacedObjectTypeSO PowerPlant;
    public PlacedObjectTypeSO Barracks;
    public Transform[] Soldiers;
    [Space]
    public GameObject pfBullet;
    public GameObject pfHitEffect;
    public GameObject pfWorldText;
    [Space]
    public GameObject SelectUnitUI;
}
