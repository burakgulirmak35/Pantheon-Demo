using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    private Vector3 startPosition;
    private List<SoldierUnit> selectedUnitList = new List<SoldierUnit>();
    [SerializeField] private Transform selectionAreaTransform;
    private void Awake()
    {
        Instance = this;
        selectionAreaTransform.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            BuildingManager.Instance.Build();
            startPosition = UtilsClass.GetMouseWorldPosition();
            selectionAreaTransform.gameObject.SetActive(true);
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 currentMousePosition = UtilsClass.GetMouseWorldPosition();
            Vector3 lowerLeft = new Vector3(
                Mathf.Min(startPosition.x, currentMousePosition.x),
                Mathf.Min(startPosition.y, currentMousePosition.y)
            );
            Vector3 upperRight = new Vector3(
                Mathf.Max(startPosition.x, currentMousePosition.x),
                Mathf.Max(startPosition.y, currentMousePosition.y)
            );
            selectionAreaTransform.position = lowerLeft;
            selectionAreaTransform.localScale = upperRight - lowerLeft;
        }
        if (Input.GetMouseButtonUp(0))
        {
            SelectUnits();
        }
        if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject())
        {
            BuildingManager.Instance.SetFlag();
            Vector3 moveToPosition = UtilsClass.GetMouseWorldPosition();

            foreach (SoldierUnit soldierUnit in selectedUnitList)
            {
                soldierUnit.MoveTo(moveToPosition);
            }

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.UnSelectBuildingType();
        }
        BuildingManager.Instance.ShowGhost();
    }

    private void SelectUnits()
    {
        selectionAreaTransform.gameObject.SetActive(false);

        Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(startPosition, UtilsClass.GetMouseWorldPosition());
        foreach (SoldierUnit soldierUnit in selectedUnitList)
        {
            soldierUnit.SetSelectedVisible(false);
        }
        selectedUnitList.Clear();
        foreach (Collider2D collider2D in collider2DArray)
        {
            SoldierUnit soldierUnit = collider2D.GetComponent<SoldierUnit>();
            if (soldierUnit != null)
            {
                soldierUnit.SetSelectedVisible(true);
                selectedUnitList.Add(soldierUnit);
            }
        }
    }

}
