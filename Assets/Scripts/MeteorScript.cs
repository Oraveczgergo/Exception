using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    public float ProjectileSpeed = 1f;
    void FixedUpdate()
    {
        transform.Translate(ProjectileSpeed * Time.deltaTime, 0, 0);
    }

    void OnEnable()
    {
        Invoke("Deactivate", 5f);
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
