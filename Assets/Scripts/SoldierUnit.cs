using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierUnit : MonoBehaviour
{
    [SerializeField] private UnitSO unitSO;
    private int unitHealth;
    private int unitPower;

    private void Awake()
    {
        unitHealth = unitSO.unitHealth;
        unitPower = unitSO.unitPower;
    }

}
