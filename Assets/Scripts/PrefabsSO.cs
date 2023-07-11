using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Prefabs")]
public class PrefabsSO : ScriptableObject
{
    public List<Transform> BuildingTypeList;
    public List<ResourceTypeSO> ResourceTypeList;
}
