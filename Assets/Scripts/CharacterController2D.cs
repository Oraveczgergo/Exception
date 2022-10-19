using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private LayerMask terrainMask;
    //public float decelaration = 10f;
    public float maxMovementSpeed = 30f;
    public float movementSpeed = 30f;
    public float normalJumpForce = 60f;
    public float groundTouchDistance;
    private float jumpAngleTreshold = 60f;    
    private HealthScript healthScript;
    private VehicleController2D vehicleController;
    private new Rigidbody2D rigidbody;
    public bool canJump = false;
    private bool needJump = false;
    private bool jumpBoost = false;
    private bool speedBoost = false;
    private bool jumpReleased = false;
    private bool canDecelerate = false;
    private float horizontal;
    private WaterScript waterScript;
    private GameObject wayPoint01;
    private GameObject wayPoint02;
    private GameObject wayPoint03;
    private GameObject wayPoint04;
    public GameObject water;
    public int CurrentSectionId = 1;

    private float speed
    {
        get
        {
            return (speedBoost ? movementSpeed * 1.5f : movementSpeed);
        }
    }
    private float maxSpeed
    {
        get
        {
            return (speedBoost ? maxMovementSpeed * 1.5f : maxMovementSpeed);
        }
    }
    private float jumpForce
    {
        get
        {
            return (jumpBoost ? normalJumpForce * 1.5f : normalJumpForce);
        }
    }
    private void Start()
    {
        wayPoint01 = GameObject.FindGameObjectWithTag("WayPoint01");
        wayPoint02 = GameObject.FindGameObjectWithTag("WayPoint02");
        wayPoint03 = GameObject.FindGameObjectWithTag("WayPoint03");
        wayPoint04 = GameObject.FindGameObjectWithTag("WayPoint04");
        rigidbody = GetComponent<Rigidbody2D>();
        healthScript = (HealthScript)GetComponent("HealthScript");
        waterScript = water.GetComponent<WaterScript>();
        vehicleController = GameObject.FindGameObjectWithTag("Vehicle").GetComponent<VehicleController2D>();        
    }
    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            needJump = true;
            Invoke("JumpInputCancel", .2f);
        }
        if (Input.GetKeyUp(KeyCode.Space) && rigidbody.velocity.y > 0f && !jumpReleased)
        {
            jumpReleased = true;
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y / 2);
        }
        if (Input.GetKeyDown(KeyCode.F1))        
            Teleport(1);
        
        if (Input.GetKeyDown(KeyCode.F2))        
            Teleport(2);           
        
        if (Input.GetKeyDown(KeyCode.F3))        
            Teleport(3);           
        
        if (Input.GetKeyDown(KeyCode.F4))        
            Teleport(4);                    
    }

    private void FixedUpdate()
    {
        if (horizontal * rigidbody.velocity.x < 0)
            rigidbody.velocity = new Vector2(rigidbody.velocity.x * 0.8f , rigidbody.velocity.y);
        if (Mathf.Abs(rigidbody.velocity.x) < maxSpeed)
            rigidbody.AddForce(new Vector2(horizontal * speed, 0), ForceMode2D.Impulse);
        if (horizontal == 0 && canDecelerate)
        {
            rigidbody.velocity *= Vector2.up;
        }
        if (canJump && needJump)
        {
            jumpReleased = false;
            CancelInvoke("JumpInputCancel");
            needJump = false;
            StopVerticalVelocity();
            rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }        
        if (rigidbody.velocity.y < -65)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, -65);            
        }
        if (transform.position.y < -150)
        {
            healthScript.Death();
        }
    }

    private void Teleport(int id)
    {
        switch (id)
        {
            case 1:
                transform.position = wayPoint01.transform.position;
                CurrentSectionId = 1;
                break;
            case 2:
                transform.position = wayPoint02.transform.position;
                CurrentSectionId = 2;
                break;
            case 3:
                transform.position = wayPoint03.transform.position;
                CurrentSectionId = 3;
                waterScript.WaterStart();
                break;
            case 4:
                transform.position = wayPoint04.transform.position;
                CurrentSectionId = 4;
                break;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {             
        if (collision.gameObject.CompareTag("Terrain"))
        {
            if (!HasCollidedWithGround(collision.contacts))
            {
                canJump = false;                
            }            
        }
    }   

    private bool HasCollidedWithGround(ContactPoint2D[] contacts)
    {
        Vector2 validDirection = Vector2.up;
        foreach (ContactPoint2D contactPoint in contacts)
        {
            if (Vector2.Angle(contactPoint.normal, validDirection) <= jumpAngleTreshold)
            {               
                canDecelerate = true;
                canJump = true;                
                return true;
            }            
        }
        return false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            canDecelerate = false;
            canJump = false;            
        }
    }

    public void RespawnPlayer()
    {
        if (vehicleController.inUse)
        {            
            vehicleController.RespawnSaucer();
        }
        rigidbody.velocity = Vector2.zero;
        Teleport(CurrentSectionId);
        healthScript.Heal(healthScript.maxHealth);
        GameObject[] powerups = GameObject.FindGameObjectsWithTag("Powerup");
        foreach (GameObject gameObject in powerups)
        {
            if (gameObject.name == "HealthPickup")
                gameObject.GetComponent<HealthPickup>().ForceRespawn();
            else if (gameObject.name == "SpeedBooster")
                gameObject.GetComponent<SpeedBooster>().ForceRespawn();
            else if (gameObject.name == "JumpBooster")
                gameObject.GetComponent<JumpBooster>().ForceRespawn();
        }
        waterScript.WaterReset();
        GameObject[] Bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject gameObject in Bullets)
        {
            gameObject.SetActive(false);
        }
    }

    private void StopVerticalVelocity()
    {
        rigidbody.velocity *= Vector2.right;
    }
    public void DoubleSpeed()
    {
        speedBoost = true;
        Invoke("SpeedReset", 10f);
    }

    public void ExtraJumpForce()
    {
        jumpBoost = true;
        Invoke("JumpReset", 10f);
    }

    private void SpeedReset()
    {
        speedBoost = false;
    }

    private void JumpReset()
    {
        jumpBoost = false;
    }

    private void JumpInputCancel()
    {
        needJump = false;
    }
}
