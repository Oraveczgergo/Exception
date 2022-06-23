using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public float SpikeKnockback = 100f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, SpikeKnockback));
            collision.gameObject.GetComponent<HealthScript>().TakeDamage(2);
        }
    }
}
