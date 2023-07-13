using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    private Vector3 StartPosition;
    private List<SoldierUnit> selectedUnitList = new List<SoldierUnit>();
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            BuildingManager.Instance.Build();
            StartPosition = UtilsClass.GetMouseWorldPosition();
        }
        if (Input.GetMouseButtonUp(0))
        {
            Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(StartPosition, UtilsClass.GetMouseWorldPosition());
            selectedUnitList.Clear();
            foreach (Collider2D collider2D in collider2DArray)
            {
                SoldierUnit soldierUnit = collider2D.GetComponent<SoldierUnit>();
                if (soldierUnit != null)
                {
                    selectedUnitList.Add(soldierUnit);
                }
            }
        }
        if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject())
        {
            BuildingManager.Instance.SetFlag();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.UnSelectBuildingType();
        }

        BuildingManager.Instance.ShowGhost();
    }

}
