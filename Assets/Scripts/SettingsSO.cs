using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Settings")]
public class SettingsSO : ScriptableObject
{
    public int PowerPlantBuildPrice;
    public int PowerPlantProductAmount;
    public float PowerPlantBuildTime;
    public float PowerPlantProductTime;
    [Space]
    public int BarracksBuildPrice;
}