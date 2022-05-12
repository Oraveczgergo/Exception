using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorHealth : MonoBehaviour
{
    public int maxHealth = 1;
    public int currentHealth = 1;
    public float invulnerabilityAfterHit = 0;
    private float damageTime = 0;

    public void TakeDamage(int x)
    {
        
        if(Time.fixedTime - damageTime > invulnerabilityAfterHit)
        {
            currentHealth -= x;
            damageTime = Time.fixedTime;
        }        
        if (currentHealth <= 0)
            Death();
    }
 
    public void Death()
    {
        gameObject.SetActive(false);
    }
}
