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
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform body;
    [Space]
    private const float speed = 20f;
    private int currentPathIndex;
    private List<Vector3> pathVectorList;
    [Space]
    private float health;
    private float healthAmount;
    private float fireRate;
    private int power;
    private float bulletForce = 10f;
    [Space]
    private GameObject selectedGameObject;
    private bool selected;

    private void Awake()
    {
        selectedGameObject = transform.Find("Selected").gameObject;
        health = unitSO.unitHealth;
        power = unitSO.power;
        fireRate = unitSO.fireRate;

        imgHealthBarFill.fillAmount = 1;
        healthAmount = 1;
    }

    private SelectedUnitUI selectedUnitUI;
    public void UnSelect()
    {
        if (selected)
        {
            selected = false;
            selectedUnitUI.gameObject.SetActive(false);
            selectedUnitUI = null;
            selectedGameObject.SetActive(false);
        }

    }
    public void Select(SelectedUnitUI _selectedUnitUI)
    {
        selected = true;
        selectedUnitUI = _selectedUnitUI;
        selectedUnitUI.gameObject.SetActive(true);
        selectedGameObject.SetActive(true);
        _selectedUnitUI.SelectUnit(unitSO.unitSprite, healthAmount);
    }

    public void MoveTo(Vector3 targetPosition)
    {
        currentPathIndex = 0;
        pathVectorList = Pathfinding.Instance.FindPath((Vector3)transform.position, targetPosition);

        if (pathVectorList != null && pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
        }
    }



    Vector2 lookDir;
    float angle;
    public void Fire(Transform _target)
    {
        target = _target;
        StartCoroutine(FireCorotine());
    }

    private void Update()
    {
        if (target != null)
        {
            Vector2 direction = target.position - body.position;
            body.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        }
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (pathVectorList != null)
        {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            if (Vector3.Distance(transform.position, targetPosition) > 0.2f)
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;
                float distanceBefore = Vector3.Distance(transform.position, targetPosition);
                transform.position = transform.position + moveDir * speed * Time.deltaTime;
            }
            else
            {
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count)
                {
                    pathVectorList = null;
                }
            }
        }
    }

    private Transform target;
    private bool isFire;
    private IEnumerator FireCorotine()
    {
        if (!isFire)
        {
            isFire = true;
            while (target != null && target.gameObject.activeSelf)
            {
                yield return new WaitForSeconds(1 / fireRate);
                if (target != null && target.gameObject.activeSelf)
                    Shoot();
            }
            StopFire();
            isFire = false;
        }
    }

    public void StopFire()
    {
        target = null;
    }

    private GameObject bullet;
    private Rigidbody2D bulletrb;
    private void Shoot()
    {
        bullet = Spawner.Instance.GetBullet();
        bullet.transform.rotation = body.rotation;
        bullet.transform.position = transform.position;
        bullet.GetComponent<Bullet>().Power = power;
        bulletrb = bullet.GetComponent<Rigidbody2D>();
        bulletrb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health >= unitSO.unitHealth)
        {
            health = unitSO.unitHealth;
            DOTween.To(() => imgHealthBarFill.fillAmount, x => imgHealthBarFill.fillAmount = x, healthAmount, 0.2f).SetEase(Ease.Linear);
        }
        else if (health <= 0)
        {
            Die();
        }
        healthAmount = health / unitSO.unitHealth;

        if (selectedUnitUI != null)
        {
            selectedUnitUI.UpdateHealth(healthAmount);
        }
    }

    private void Die()
    {
        health = 0;
        imgHealthBarFill.fillAmount = 0;
        UnSelect();
        GameController.Instance.RemoveSelectedUnit(this);
        Destroy(gameObject);
    }
}
