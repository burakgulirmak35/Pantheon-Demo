using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject hitEffect;

    private void OnCollisionEnter2D(Collision2D other)
    {
        //hitEffect = Spawner.Instance.GetHitEffect();
        //hitEffect.transform.position = transform.position;
        //hitEffect.SetActive(true);
        this.gameObject.SetActive(false);
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
