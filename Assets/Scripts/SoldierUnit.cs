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
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform body;
    private HealthSystem healthSystem;
    [Space]
    private int currentPathIndex;
    private List<Vector3> pathVectorList;
    [Space]
    private float fireRate;
    private int power;
    private float bulletForce = 10f;
    [Space]
    private GameObject selectedGameObject;
    private bool selected;
    private float walkSpeed;

    private void Awake()
    {
        walkSpeed = unitSO.walkSpeed;
        selectedGameObject = transform.Find("Selected").gameObject;
        power = unitSO.power;
        fireRate = unitSO.fireRate;
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.SetHealth(unitSO.unitHealth);
        healthSystem.OnDied += OnDie;
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
        _selectedUnitUI.SelectUnit(unitSO.unitSprite, healthSystem.healthAmount);
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
    private void Update()
    {
        if (target != null)
        {
            Vector2 direction = target.position - body.position;
            body.rotation = Quaternion.FromToRotation(Vector3.up, direction);
            body.rotation = Quaternion.Euler(body.eulerAngles.x, 0, body.eulerAngles.z);
        }
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (pathVectorList != null)
        {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            if (Vector3.Distance(transform.position, targetPosition) > 0.2f && !isFire)
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;
                float distanceBefore = Vector3.Distance(transform.position, targetPosition);
                transform.position = transform.position + moveDir * walkSpeed * Time.deltaTime;
                body.rotation = Quaternion.FromToRotation(Vector3.up, moveDir);
                body.rotation = Quaternion.Euler(body.eulerAngles.x, 0, body.eulerAngles.z);
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
    private Coroutine FireCoro;

    public void Fire(Transform _target)
    {
        target = _target;
        if (FireCoro == null)
        {
            FireCoro = StartCoroutine(FireLoop());
        }
    }

    private IEnumerator FireLoop()
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
        }
    }

    public void StopFire()
    {
        if (FireCoro != null)
        {
            StopCoroutine(FireCoro);
            FireCoro = null;
        }
        target = null;
        isFire = false;
    }

    private GameObject bullet;
    private Rigidbody2D bulletrb;
    private void Shoot()
    {
        bullet = Spawner.Instance.GetBullet();
        bullet.transform.rotation = body.rotation;
        bullet.transform.position = firePoint.position;
        bullet.GetComponent<Bullet>().Power = power;
        bulletrb = bullet.GetComponent<Rigidbody2D>();
        bulletrb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

    private void OnDamaged()
    {
        if (selectedUnitUI != null)
        {
            selectedUnitUI.UpdateHealth(healthSystem.healthAmount);
        }
    }

    private void OnDie(object sender, System.EventArgs e)
    {
        UnSelect();
        GameController.Instance.RemoveSelectedUnit(this);
    }
}
