using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class PowerPlant : MonoBehaviour
{
    [SerializeField] private SettingsSO settings;
    [Space]
    [SerializeField] private Image imgProgressFill;
    [Space]
    [SerializeField] private Image imgHealthBarFill;
    private float health;

    private void Start()
    {
        UIManager.Instance.SpendResource(settings.PowerPlantBuildPrice);
        UIManager.Instance.UnSelectBuildingType();
        StartCoroutine(GenerateResourceCoro());
        imgHealthBarFill.fillAmount = 1;
        health = settings.PowerPlantHealth;
    }

    private void OnMouseDown()
    {
        BuildingManager.Instance.SelectBuilding(this);
    }

    public void AddHealth(float amount)
    {
        health += amount;
        if (health >= settings.PowerPlantHealth)
        {
            health = settings.PowerPlantHealth;
        }
        else if (health <= 0)
        {
            health = 0;
        }
        DOTween.To(() => imgHealthBarFill.fillAmount, x => imgHealthBarFill.fillAmount = x, health / settings.PowerPlantHealth, 0.25f).SetEase(Ease.Linear);
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
