using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorCollision : MonoBehaviour
{
    private HealthScript healthScript;
    private void Start()
    {
        healthScript = (HealthScript)gameObject.GetComponent("HealthScript");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Meteor")
        {
            collision.gameObject.SetActive(false);
            healthScript.TakeDamage(10);
        }
    }
}
