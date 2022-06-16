using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementLock : MonoBehaviour
{
    private VehicleController2D vehicleController2D;
    void Start()
    {
        vehicleController2D = GameObject.FindGameObjectWithTag("Vehicle").GetComponent<VehicleController2D>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Vehicle"))
            vehicleController2D.VehicleBackInCamBounds();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Vehicle"))
        {
            GameObject saucer = collision.gameObject;
            if (saucer.transform.position.y > transform.position.y)
                vehicleController2D.VehicleAboveCamBounds();
            else if (saucer.GetComponent<VehicleController2D>().inUse)
                vehicleController2D.VehicleBelowCamBounds();
        }
    }
}
