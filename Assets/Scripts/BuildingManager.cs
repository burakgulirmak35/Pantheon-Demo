using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private PrefabsSO prefabs;
    private Camera mainCamera;
    private Transform building;
    [Header("Holders")]
    [SerializeField] private Transform HarvesterHolder;

    private void Awake()
    {
        building = prefabs.BuildingTypeList[0];
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(building, GetMouseWorldPosition(), Quaternion.identity, HarvesterHolder);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            building = prefabs.BuildingTypeList[0];
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            building = prefabs.BuildingTypeList[1];
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }
}
