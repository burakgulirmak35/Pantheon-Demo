using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Barracks : MonoBehaviour
{
    [SerializeField] private SettingsSO settings;
    [SerializeField] private PrefabsSO prefabs;
    [SerializeField] private PlacedObjectTypeSO buildingType;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform firstPoint;
    private SoldierUnit soldierUnit;
    private HealthSystem healthSystem;

    private void Start()
    {
        UIManager.Instance.SpendResource(settings.BarracksBuildPrice);
        UIManager.Instance.UnSelectBuildingType();
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.SetHealth(buildingType.Health);
    }

    private void OnMouseDown()
    {
        GridBuildingSystem.Instance.SelectBuilding(this);
    }

    public void SpawnUnit(int _unitID)
    {
        soldierUnit = Instantiate(prefabs.Soldiers[_unitID], spawnPoint.position, Quaternion.identity).GetComponent<SoldierUnit>();
        soldierUnit.MoveTo(firstPoint.position);

    }
}
