using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float projectileSpeed = 60f;
    public float duration = 2f;
    void FixedUpdate()
    {
        transform.Translate(projectileSpeed * Time.deltaTime, 0, 0);
    }                                           

    void OnEnable()
    {
        Invoke("Deactivate", duration);
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
