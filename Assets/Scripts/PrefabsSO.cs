using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObjects/Prefabs")]
public class PrefabsSO : ScriptableObject
{
    public Transform PowerPlant;
    public Transform Barracks;
    [Space]
    public Sprite PowerPlantSprite;
    public Sprite BarracksSprite;
}
