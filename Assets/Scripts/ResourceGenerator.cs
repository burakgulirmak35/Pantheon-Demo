using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    [SerializeField] private BuildingTypeSO buildingType;
    private IEnumerator GenerateResourceCoro()
    {
        while (true)
        {
            yield return new WaitForSeconds(buildingType.timeToGenerate);
            ResourceManager.Instance.AddResource(buildingType.resourceType, 1);
        }
    }
}
