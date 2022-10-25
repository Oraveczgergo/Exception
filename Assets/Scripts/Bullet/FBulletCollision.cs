using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBulletCollision : MonoBehaviour
{
    private MeteorHealth meteorHealth;
    private void Start()
    {
        meteorHealth = (MeteorHealth)gameObject.GetComponent("MeteorHealth");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "FBullet")
        {
            collision.gameObject.SetActive(false);           
            meteorHealth.TakeDamage(1);
        }
    }
}
