using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlant : MonoBehaviour
{
    [SerializeField] private SettingsSO settings;

    private void Start()
    {
        UIManager.Instance.AddResource(-settings.PowerPlantBuildPrice);
        UIManager.Instance.UnSelectBuildingType();
        StartCoroutine(GenerateResourceCoro());
    }

    private IEnumerator GenerateResourceCoro()
    {
        while (true)
        {
            yield return new WaitForSeconds(settings.PowerPlantProductTime);
            UIManager.Instance.AddResource(settings.PowerPlantProductAmount);
        }
    }

}
