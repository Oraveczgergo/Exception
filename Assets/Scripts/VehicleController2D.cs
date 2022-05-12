using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController2D : MonoBehaviour
{
    public float movementSpeedInput = 30;
    public float jumpForceInput = 60;
    private float movementSpeed;
    private float jumpForce;
    private bool inUse = false;
    private GameObject player;
    private GameObject healthText;
    private GameObject vehicleHealthText;
    private CameraScript mainCamera;
    private GenericObjectPooler objectPooler;
    private VehicleHealthScript vehicleHealthScript;

    private new Rigidbody2D rigidbody;

    void Start()
    {
        objectPooler = (GenericObjectPooler)GameObject.FindGameObjectWithTag("OP_FBullet").GetComponent("GenericObjectPooler");
        rigidbody = GetComponent<Rigidbody2D>();
        movementSpeed = movementSpeedInput;
        jumpForce = jumpForceInput;
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = (CameraScript)GameObject.FindGameObjectWithTag("MainCamera").GetComponent("CameraScript");
        healthText = GameObject.FindGameObjectWithTag("UI_MainHealthText");
        vehicleHealthText = GameObject.FindGameObjectWithTag("UI_VehicleHealthText");
        vehicleHealthText.SetActive(false);
        vehicleHealthScript = (VehicleHealthScript)GetComponent("VehicleHealthScript");
    }

    void Update()
    {
        if (inUse)
        {
            healthText.SetActive(false);
            vehicleHealthText.SetActive(true);
            var movement = Input.GetAxis("Horizontal");
            transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * movementSpeed;
            if (Input.GetButtonDown("Jump"))
            {
                rigidbody.velocity = (new Vector2(0, jumpForce));
            }
            //player.transform.position = transform.position;
        }
        if (Input.GetButtonDown("Fire1") && inUse)
        {
            Fire();
        }
        if (transform.position.y < -150)
        {
            vehicleHealthScript.TakeDamage(999);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inUse = true;          
            player.SetActive(false);
            mainCamera.followedObject = gameObject;
        }
    }

    public void LeaveVehicle()
    {
        if(inUse)
        {
            inUse = false;
            healthText.SetActive(true);
            vehicleHealthText.SetActive(false);
            player.SetActive(true);
            player.transform.position = transform.position + new Vector3(20, 0);
            mainCamera.followedObject = player;
        }        
    }

    private void Fire()
    {
        GameObject gameObject = objectPooler.GetPooledObject();
        if (gameObject == null) return;
        RotateToCursor(gameObject.transform);
        gameObject.transform.position = transform.position;
        gameObject.SetActive(true);
    }

    private void RotateToCursor(Transform o)
    {
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();
        float rotation_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        o.transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);
    }
}
