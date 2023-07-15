using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GridBuildingSystem : MonoBehaviour
{
    [SerializeField] private PrefabsSO prefabs;
    [SerializeField] private GameObject GhostObject;
    [SerializeField] private Transform BuildingHolder;
    [SerializeField] Transform building;
    public static GridBuildingSystem Instance { get; private set; }
    [HideInInspector] public Barracks ChoosenBarraks;

    private Grid<GridObject> grid;
    private void Awake()
    {
        int gridWidth = 40;
        int gridHeight = 40;
        float cellSize = 1f;

        Instance = this;
        grid = new Grid<GridObject>(gridWidth, gridHeight, cellSize, new Vector3(-(gridWidth * cellSize) / 2, -(gridHeight * cellSize) / 2, 0), (Grid<GridObject> g, int x, int y) => new GridObject(g, x, y));
    }

    public void Build()
    {
        if (building != null)
        {
            grid.GetXY(UtilsClass.GetMouseWorldPosition(), out int x, out int z);

            GridObject gridObject = grid.GetGridObject(x, z);
            if (gridObject.CanBuild())
            {
                Transform buildTransform = Instantiate(building, grid.GetWorldPosition(x, z), Quaternion.identity, BuildingHolder);
                gridObject.SetTransform(buildTransform);
            }
            else
            {
                Spawner.Instance.CreateWorldTextPopup("Cannot build here!", UtilsClass.GetMouseWorldPosition());
            }

        }
    }

    public void ShowGhost()
    {
        if (building != null)
        {
            GhostObject.transform.position = UtilsClass.GetMouseWorldPosition();
        }
    }

    public void SelectPowerPlant()
    {
        building = prefabs.PowerPlant;
        GhostObject.GetComponent<SpriteRenderer>().sprite = prefabs.PowerPlantSprite;
        GhostObject.gameObject.SetActive(true);
    }

    public void SelectBarrracks()
    {
        building = prefabs.Barracks;
        GhostObject.GetComponent<SpriteRenderer>().sprite = prefabs.BarracksSprite;
        GhostObject.SetActive(true);
    }

    public void UnSelect()
    {
        building = null;
        GhostObject.SetActive(false);
    }

    public void SelectBuilding(PowerPlant powerPlant)
    {
        UIManager.Instance.SelectPowerPlant();
    }

    public void SelectBuilding(Barracks barracks)
    {
        ChoosenBarraks = barracks;
        UIManager.Instance.SelectBarracks();
    }
















    public class GridObject
    {
        private Grid<GridObject> grid;
        private int x;
        private int y;
        private Transform transform;

        public GridObject(Grid<GridObject> grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
        }

        public void SetTransform(Transform transform)
        {
            this.transform = transform;
            grid.TriggerGridObjectChanged(x, y);
        }

        public void ClearTransform()
        {
            transform = null;
            grid.TriggerGridObjectChanged(x, y);
        }

        public bool CanBuild()
        {
            return transform == null;
        }

        public override string ToString()
        {
            return x + ", " + y + "\n" + transform;
        }
    }
}

