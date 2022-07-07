using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {            
            collision.gameObject.GetComponent<HealthScript>().TakeSpikeDamage(2);
        }
    }
}
