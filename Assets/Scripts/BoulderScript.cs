using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.CompareTag("Player"))
        {
            collision.collider.GetComponent<HealthScript>().Death();
        }
    }

    private void FixedUpdate()
    {
        if (transform.position.y < -150)
        {
            gameObject.SetActive(false);
        }
    }
}
