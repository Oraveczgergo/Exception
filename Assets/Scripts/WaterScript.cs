using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<HealthScript>().Death();
        }
        if (collision.gameObject.tag == "Vehicle")
        {
            collision.gameObject.GetComponent<VehicleHealthScript>().Death();
        }
    }
}
