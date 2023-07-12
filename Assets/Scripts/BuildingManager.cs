using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private PrefabsSO prefabs;
    [Space]
    [SerializeField] private GameObject GhostObject;
    private Transform building;

    [Header("Holders")]
    [SerializeField] private Transform BuildingHolder;


    public static BuildingManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (building != null)
            {
                if (CanSpawnBuilding(building, UtilsClass.GetMouseWorldPosition()))
                {
                    Instantiate(building, UtilsClass.GetMouseWorldPosition(), Quaternion.identity, BuildingHolder);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.UnSelectBuildingType();
        }

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

    private bool CanSpawnBuilding(Transform _building, Vector3 _position)
    {
        BoxCollider2D boxCollider2D = _building.GetComponent<BoxCollider2D>();
        Collider2D[] collider2DArray = Physics2D.OverlapBoxAll(_position + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0);
        return collider2DArray.Length == 0;
    }

}
