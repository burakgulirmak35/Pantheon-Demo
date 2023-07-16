using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject hitEffect;
    [HideInInspector] public float Power;

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Enemy":
                Hit();
                other.GetComponent<SoldierUnit>().TakeDamage(Power);
                break;
            case "Barracks":
                Hit();
                other.GetComponent<Barracks>().TakeDamage(Power);
                break;
            case "PowerPlant":
                Hit();
                other.GetComponent<PowerPlant>().TakeDamage(Power);
                break;
            default:
                // Hit();
                break;
        }
    }

    private void Hit()
    {
        //hitEffect = Spawner.Instance.GetHitEffect();
        //hitEffect.transform.position = transform.position;
        //hitEffect.SetActive(true);
        gameObject.SetActive(false);
        if (DissaperCoro != null)
        {
            StopCoroutine(DissaperCoro);
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
    }

    public void ResetBullet()
    {
        if (DissaperCoro != null)
        {
            StopCoroutine(DissaperCoro);
        }
        DissaperCoro = StartCoroutine(DissapearTimer());
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    private Coroutine DissaperCoro;
    private IEnumerator DissapearTimer()
    {
        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(false);
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }
}
