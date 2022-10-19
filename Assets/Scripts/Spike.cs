using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{    
    public int damage = 5;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {            
            collision.gameObject.GetComponent<HealthScript>().TakeSpikeDamage(damage);
        }
    }
}
