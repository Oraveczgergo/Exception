using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float ProjectileSpeed = 60f;
    void FixedUpdate()
    {
        transform.Translate(ProjectileSpeed * Time.deltaTime, 0, 0);
    }                                           

    void OnEnable()
    {
        Invoke("Deactivate", 2f);
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }
    void OnDisable()
    {
        CancelInvoke();
    }
}
