using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private HealthScript healthScript;
    public int healAmount = 10;
    private void Start()
    {
        healthScript = (HealthScript)GameObject.FindGameObjectWithTag("Player").GetComponent("HealthScript");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
            healthScript.Heal(healAmount);
            Invoke("Reactivate", 10f);
        }
    }

    private void Reactivate()
    {
        gameObject.SetActive(true);
    }
}
