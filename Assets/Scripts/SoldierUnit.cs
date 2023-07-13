using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldierUnit : MonoBehaviour
{
    [SerializeField] private UnitSO unitSO;
    [Space]
    private int unitHealth;
    private int unitPower;
    [Space]
    private GameObject selectedGameObject;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        selectedGameObject = transform.Find("Selected").gameObject;
        unitHealth = unitSO.unitHealth;
        unitPower = unitSO.unitPower;
    }

    public void SetSelectedVisible(bool visible)
    {
        selectedGameObject.SetActive(visible);
    }

    public void MoveTo(Vector3 targetPosition)
    {
        agent.SetDestination(targetPosition);
    }

}
