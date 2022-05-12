using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleHealthScript : HealthScript
{
    private void Start()
    {
         maxHealth = 100;
         currentHealth = 100;
    }    

    private void LateUpdate()
    {
        healthText.text = "Vehicle Health: " + currentHealth.ToString();
    }
}

