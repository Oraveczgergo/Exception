using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    public float ProjectileSpeed = 1f;
    public ParticleSystem SmokeEffect;
    private bool canMove = true;
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
        Invoke("Deactivate", 5f);
    }

    public void Deactivate()
    {
        if (SmokeEffect != null)
            SmokeEffect.Stop();
        Invoke("Disable", 8f);
        canMove = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
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
