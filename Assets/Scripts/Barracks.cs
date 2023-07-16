using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Barracks : MonoBehaviour
{
    [SerializeField] private SettingsSO settings;
    [SerializeField] private PrefabsSO prefabs;
    [SerializeField] private Image imgHealthBarFill;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform firstPoint;
    private SoldierUnit soldierUnit;
    private float health;

    private void Start()
    {
        UIManager.Instance.SpendResource(settings.BarracksBuildPrice);
        UIManager.Instance.UnSelectBuildingType();

        imgHealthBarFill.fillAmount = 1;
        health = settings.BarracksHealth;
    }

    private void OnMouseDown()
    {
        GridBuildingSystem.Instance.SelectBuilding(this);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health >= settings.BarracksHealth)
        {
            health = settings.BarracksHealth;
            DOTween.To(() => imgHealthBarFill.fillAmount, x => imgHealthBarFill.fillAmount = x, health / settings.BarracksHealth, 0.25f).SetEase(Ease.Linear);
        }
        else if (health <= 0)
        {
            health = 0;
            imgHealthBarFill.fillAmount = 0;
            GridBuildingSystem.Instance.ClearArea(myGridPositionList);
            Destroy(gameObject);
            return;
        }
        DOTween.To(() => imgHealthBarFill.fillAmount, x => imgHealthBarFill.fillAmount = x, health / settings.BarracksHealth, 0.25f).SetEase(Ease.Linear);
    }

    private List<Vector2Int> myGridPositionList;
    public void SetGridList(List<Vector2Int> _gridPositionList)
    {
        myGridPositionList = _gridPositionList;
    }

    public void SpawnUnit(int _unitID)
    {
        soldierUnit = Instantiate(prefabs.Soldiers[_unitID], spawnPoint.position, Quaternion.identity).GetComponent<SoldierUnit>();
        soldierUnit.MoveTo(firstPoint.position);
    }
}
