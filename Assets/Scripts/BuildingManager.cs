using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private PrefabsSO prefabs;
    private Camera mainCamera;
    private Transform building;
    [Header("Holders")]
    [SerializeField] private Transform BuildingHolder;

    private void Awake()
    {
        building = prefabs.PowerPlant;
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (building != null)
            {
                Instantiate(building, GetMouseWorldPosition(), Quaternion.identity, BuildingHolder);
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            building = prefabs.PowerPlant;
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            building = prefabs.PowerPlant;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }
}
