using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDamaged;
    public event EventHandler OnDied;

    [SerializeField] private Image imgHealthBarFill;
    private float startHealth;
    private float health;
    [HideInInspector] public float healthAmount;

    public void SetHealth(float _health)
    {
        startHealth = _health;
        health = _health;
        healthAmount = 1f;
        imgHealthBarFill.fillAmount = healthAmount;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        OnDamaged?.Invoke(this, EventArgs.Empty);

        if (health >= startHealth)
        {
            health = startHealth;
        }
        else if (health <= 0)
        {
            health = 0;
            imgHealthBarFill.fillAmount = 0;
            if (myGridPositionList != null)
            {
                GridBuildingSystem.Instance.ClearArea(myGridPositionList);
            }
            OnDied?.Invoke(this, EventArgs.Empty);
            Destroy(gameObject);
            return;
        }
        healthAmount = health / startHealth;
        DOTween.To(() => imgHealthBarFill.fillAmount, x => imgHealthBarFill.fillAmount = x, healthAmount, 0.25f).SetEase(Ease.Linear);
    }

    private List<Vector2Int> myGridPositionList;
    public void SetGridList(List<Vector2Int> _gridPositionList)
    {
        myGridPositionList = _gridPositionList;
    }

}
