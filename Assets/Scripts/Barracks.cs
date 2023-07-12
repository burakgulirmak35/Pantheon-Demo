using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : MonoBehaviour
{
    [SerializeField] private SettingsSO settings;

    private void Start()
    {
        UIManager.Instance.AddResource(-settings.BarracksBuildPrice);
        UIManager.Instance.UnSelectBuildingType();
    }
}
