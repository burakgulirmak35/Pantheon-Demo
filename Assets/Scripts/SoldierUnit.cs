using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using DG.Tweening;

public class SoldierUnit : MonoBehaviour
{
    [SerializeField] private UnitSO unitSO;
    [Space]
    [SerializeField] private Image imgHealthBarFill;
    [Space]
    private float health;
    private float fireRate;
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
        health = unitSO.unitHealth;
        unitPower = unitSO.unitPower;
        fireRate = unitSO.fireRate;
    }

    public void SetSelectedVisible(bool visible)
    {
        selectedGameObject.SetActive(visible);
    }

    public void MoveTo(Vector3 targetPosition)
    {
        agent.SetDestination(targetPosition);
    }

    public void Fire()
    {

    }

    public void AddHealth(float amount)
    {
        health += amount;
        if (health >= unitSO.unitHealth)
        {
            health = unitSO.unitHealth;
        }
        else if (health <= 0)
        {
            health = 0;
        }
        DOTween.To(() => imgHealthBarFill.fillAmount, x => imgHealthBarFill.fillAmount = x, health / unitSO.unitHealth, 0.2f).SetEase(Ease.Linear);
    }


}
