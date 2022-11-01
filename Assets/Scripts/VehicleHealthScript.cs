using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleHealthScript : HealthScript
{
    private VehicleController2D controller;

    private void Start()
    {
         controller = GetComponent<VehicleController2D>();
         maxHealth = 100;
         currentHealth = 100;
    }
    public new void TakeDamage(int x)
    {        
        currentHealth -= x;       
        if (currentHealth <= 0)
            Death();
    }
    private void LateUpdate()
    {
        healthText.text = "Vehicle Health: " + currentHealth.ToString() + " / " + maxHealth.ToString();
    }

    public new void Death()
    {
        controller.RespawnSaucer();
    }
}

