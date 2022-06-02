using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    public int FramesForUpdate = 10;
    private int i = 0;
    private void FixedUpdate()
    { //228
        if (transform.localScale.y < 228)
        {
            i++;
            if (i >= FramesForUpdate)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + 1, transform.localScale.z);
                i = 0;
            }
        }
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
