using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PowerPlant : MonoBehaviour
{
    [SerializeField] private SettingsSO settings;
    [Space]
    [SerializeField] private Image imgFill;
    [SerializeField] private TextMeshPro txtIncome;

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
