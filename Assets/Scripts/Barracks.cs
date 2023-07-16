using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : MonoBehaviour
{
    [SerializeField] private SettingsSO settings;
    [SerializeField] private PrefabsSO prefabs;
    [SerializeField] private PlacedObjectTypeSO buildingType;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform flag;
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
        ShowFlag(true);
    }

    public void ShowFlag(bool state)
    {
        flag.gameObject.SetActive(state);
    }

    public void PlaceFlag(Vector2 pos)
    {
        flag.position = pos;
    }

    public void SpawnUnit(int _unitID)
    {
        soldierUnit = Instantiate(prefabs.Soldiers[_unitID], spawnPoint.position, Quaternion.identity).GetComponent<SoldierUnit>();
        soldierUnit.MoveTo(flag.position);
    }
}
