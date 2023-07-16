using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PowerPlant : MonoBehaviour
{
    [SerializeField] private SettingsSO settings;
    [SerializeField] private PlacedObjectTypeSO buildingType;
    [SerializeField] private Image imgProgressFill;

    private void Start()
    {
        UIManager.Instance.SpendResource(settings.PowerPlantBuildPrice);
        UIManager.Instance.UnSelectBuildingType();
        StartCoroutine(GenerateResourceCoro());
        GetComponent<HealthSystem>().SetHealth(buildingType.Health);
    }

    private void OnMouseDown()
    {
        GridBuildingSystem.Instance.SelectBuilding(this);
    }

    private IEnumerator GenerateResourceCoro()
    {
        while (true)
        {
            imgProgressFill.fillAmount = 0;
            DOTween.To(() => imgProgressFill.fillAmount, x => imgProgressFill.fillAmount = x, (float)1f, settings.PowerPlantProductTime).SetEase(Ease.Linear);
            yield return new WaitForSeconds(settings.PowerPlantProductTime);
            UIManager.Instance.AddResource(settings.PowerPlantProductAmount);
        }
    }

}
