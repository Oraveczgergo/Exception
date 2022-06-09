using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController2D : MonoBehaviour
{
    public float jumpForceInput = 60;
    public float movementSpeed;
    private float jumpForce;
    private bool inUse = false;
    private GameObject player;
    private GameObject healthText;
    private GameObject vehicleHealthText;
    private CameraScript mainCamera;
    private GenericObjectPooler objectPooler;
    private VehicleHealthScript vehicleHealthScript;
    private GameObject[] backgroundImages;
    private VehicleCameraScript vehicleCameraScript;
    private GameObject vehicleCamera;

    private new Rigidbody2D rigidbody;

    void Start()
    {
        backgroundImages = GameObject.FindGameObjectsWithTag("BackgroundImage");
        objectPooler = (GenericObjectPooler)GameObject.FindGameObjectWithTag("OP_FBullet").GetComponent("GenericObjectPooler");
        vehicleCamera = GameObject.FindGameObjectWithTag("VehicleCamera");
        vehicleCameraScript = vehicleCamera.GetComponent<VehicleCameraScript>();
        rigidbody = GetComponent<Rigidbody2D>();
        jumpForce = jumpForceInput;
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = (CameraScript)GameObject.FindGameObjectWithTag("MainCamera").GetComponent("CameraScript");
        healthText = GameObject.FindGameObjectWithTag("UI_MainHealthText");
        vehicleHealthText = GameObject.FindGameObjectWithTag("UI_VehicleHealthText");
        vehicleHealthText.SetActive(false);
        vehicleHealthScript = (VehicleHealthScript)GetComponent("VehicleHealthScript");
        vehicleCameraScript.SyncSpeed(movementSpeed);
    }

    void Update()
    {
        if (inUse)
        {
            healthText.SetActive(false);
            vehicleHealthText.SetActive(true);
            //if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            //{
                var movement = Input.GetAxis("Horizontal");
                switch (movement)
                {
                    case 0:
                        NormalMovement();
                        vehicleCameraScript.NormalMovement();
                        break;
                    case 1:
                        FastMovement();
                        vehicleCameraScript.FastMovement();
                        break;
                    case -1:
                        SlowMovement();
                        vehicleCameraScript.SlowMovement();
                        break;
                }
            //}
            //transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * movementSpeed;
            if (Input.GetButtonDown("Jump"))
            {
                rigidbody.velocity *= Vector2.right;
                rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
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
    private void NormalMovement()
    {
        rigidbody.velocity *= Vector2.up;
        rigidbody.AddForce(new Vector2(movementSpeed, 0), ForceMode2D.Impulse);
    }

    private void FastMovement()
    {
        rigidbody.velocity *= Vector2.up;
        rigidbody.AddForce(new Vector2(movementSpeed * 1.5f, 0), ForceMode2D.Impulse);
    }

    private void SlowMovement()
    {
        rigidbody.velocity *= Vector2.up;
        rigidbody.AddForce(new Vector2(movementSpeed / 1.5f, 0), ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inUse = true;
            player.SetActive(false);
            vehicleCamera.transform.position = transform.position;
            mainCamera.followedObject = vehicleCamera;
            foreach (GameObject backgroundImage in backgroundImages)
            {
                backgroundImage.GetComponent<Background>().followedObject = vehicleCamera;
            }
        }
    }

    public void LeaveVehicle()
    {
        if (inUse)
        {
            transform.position = GameObject.FindGameObjectWithTag("VehicleLandPoint").transform.position;
            inUse = false;
            healthText.SetActive(true);
            vehicleHealthText.SetActive(false);
            player.SetActive(true);
            player.transform.position = transform.position + new Vector3(20, 0);
            mainCamera.followedObject = player;
            foreach (GameObject backgroundImage in backgroundImages)
            {
                backgroundImage.GetComponent<Background>().followedObject = player;
            }
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
