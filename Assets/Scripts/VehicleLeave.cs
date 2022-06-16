using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleLeave : MonoBehaviour
{
    private VehicleController2D vehicleController;
    private GameObject vehicleCamera;

    private void Start()
    {
        vehicleController = GameObject.FindGameObjectWithTag("Vehicle").GetComponent<VehicleController2D>();
        vehicleCamera = GameObject.FindGameObjectWithTag("VehicleCamera");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Vehicle" && vehicleController.inUse)
        {
            vehicleController.inUse = false;
            vehicleCamera.SetActive(false);
            vehicleController = (VehicleController2D)collision.gameObject.GetComponent("VehicleController2D");
            vehicleController.LeaveVehicle();
        }
    }
}
