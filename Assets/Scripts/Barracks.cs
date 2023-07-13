using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Barracks : MonoBehaviour
{
    [SerializeField] private SettingsSO settings;
    [SerializeField] private PrefabsSO prefabs;
    [Space]
    [SerializeField] private Image imgHealthBarFill;
    [Space]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform flag;
    private float health;
    private SoldierUnit soldierUnit;

    private void Start()
    {
        UIManager.Instance.SpendResource(settings.BarracksBuildPrice);
        UIManager.Instance.UnSelectBuildingType();

        imgHealthBarFill.fillAmount = 1;
        health = settings.BarracksHealth;
    }

    private void OnMouseDown()
    {
        BuildingManager.Instance.SelectBuilding(this);
        flag.gameObject.SetActive(true);
    }

    public void SetFlag(Vector3 _position)
    {
        flag.position = _position;
    }

    public void UnSelect()
    {
        flag.gameObject.SetActive(false);
    }

    public void AddHealth(float amount)
    {
        health += amount;
        if (health >= settings.BarracksHealth)
        {
            health = settings.BarracksHealth;
        }
        else if (health <= 0)
        {
            health = 0;
        }
        DOTween.To(() => imgHealthBarFill.fillAmount, x => imgHealthBarFill.fillAmount = x, health / settings.BarracksHealth, 0.25f).SetEase(Ease.Linear);
    }

    public void SpawnUnit(int _unitID)
    {
        soldierUnit = Instantiate(prefabs.Soldiers[_unitID], spawnPoint.position, Quaternion.identity).GetComponent<SoldierUnit>();
        soldierUnit.MoveTo(flag.position);
    }
}
