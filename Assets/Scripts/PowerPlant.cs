using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlant : MonoBehaviour
{
    [SerializeField] private SettingsSO settings;

    private void Start()
    {
        StartCoroutine(GenerateResourceCoro());
    }

    private IEnumerator GenerateResourceCoro()
    {
        while (true)
        {
            yield return new WaitForSeconds(settings.PowerPlantProductTime);
            UIManager.Instance.AddPower(settings.PowerPlantProductAmount);
        }
    }

}
