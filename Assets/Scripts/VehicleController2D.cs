using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController2D : MonoBehaviour
{
    public float jumpForceInput = 60;
    public float movementSpeed;
    private float jumpForce;
    public bool inUse = false;
    private bool disabled = false;
    private bool disableJump = false;
    private GameObject player;
    private GameObject healthText;
    private GameObject vehicleHealthText;
    private CameraScript mainCamera;
    private GenericObjectPooler objectPooler;
    private VehicleHealthScript vehicleHealthScript;
    private GameObject[] backgroundImages;
    private VehicleCameraScript vehicleCameraScript;
    private GameObject vehicleCamera;
    private GameObject saucerRespawnSpot;

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
        saucerRespawnSpot = GameObject.FindGameObjectWithTag("SaucerSpot");
        vehicleCameraScript.SyncSpeed(movementSpeed);
    }

    void Update()
    {
        if (inUse)
        {
            
            //gameObject.GetComponent<SpriteRenderer>().color = Color.white;
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
            if (Input.GetButtonDown("Jump") && !disableJump)
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
            if (inUse)
                vehicleHealthScript.Death();
            else
                gameObject.SetActive(false);
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
        rigidbody.AddForce(new Vector2(movementSpeed * 2f, 0), ForceMode2D.Impulse);
    }

    private void SlowMovement()
    {
        rigidbody.velocity *= Vector2.up;
        rigidbody.AddForce(new Vector2(movementSpeed / 2f, 0), ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !disabled)
        {
            inUse = true;
            disabled = true;
            healthText.SetActive(false);
            vehicleHealthText.SetActive(true);
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
        vehicleCamera.SetActive(false);
        transform.position = GameObject.FindGameObjectWithTag("VehicleLandPoint").transform.position;
        rigidbody.velocity = Vector2.zero;
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

    private void Fire()
    {
        GameObject gameObject = objectPooler.GetPooledObject();
        if (gameObject == null) return;
        RotateToCursor(gameObject.transform);
        gameObject.transform.position = transform.position;
        gameObject.SetActive(true);
    }
    public void RespawnSaucer()
    {
        transform.position = saucerRespawnSpot.transform.position;
        rigidbody.velocity *= Vector2.right;
        vehicleHealthScript.Heal(vehicleHealthScript.maxHealth);
        vehicleCameraScript.ResetVehicleCamera();
        GameObject[] Meteorites = GameObject.FindGameObjectsWithTag("Meteor");
        foreach (GameObject gameObject in Meteorites)
        {
            gameObject.SetActive(false);
        }        
    }

    private void RotateToCursor(Transform o)
    {
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();
        float rotation_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        o.transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);
    }

    public void VehicleAboveCamBounds()
    {
        disableJump = true;
    }

    public void VehicleBackInCamBounds()
    {
        disableJump = false;
    }

    public void VehicleBelowCamBounds()
    {
        vehicleHealthScript.Death();
    }
}
