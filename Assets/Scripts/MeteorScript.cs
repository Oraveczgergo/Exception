using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    public float ProjectileSpeed = 1f;
    public ParticleSystem SmokeEffect;
    public bool canMove = true;
    void FixedUpdate()
    {
        if (canMove)
            transform.Translate(ProjectileSpeed * Time.deltaTime, 0, 0);
    }

    void OnEnable()
    {
        if (SmokeEffect != null)
            SmokeEffect.Play();
        canMove = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        Invoke("Deactivate", 10f);
    }

    public void Deactivate()
    {
        if (SmokeEffect != null)
            SmokeEffect.Stop();
        Invoke("Disable", 8f);
        canMove = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
    }
    void Disable()
    {
        gameObject.SetActive(false);
    }
    void OnDisable()
    {
        CancelInvoke();
    }
}
