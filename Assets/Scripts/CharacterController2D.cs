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
    public float bounceForce = 5f;
    private HealthScript healthScript;
    private new Rigidbody2D rigidbody;
    public bool canJump = false;
    private bool needJump = false;
    private bool jumpBoost = false;
    private bool speedBoost = false;
    private bool jumpReleased = false;
    private bool canDecelerate = false;
    //private bool onGround = false;
    private float horizontal;
    private CircleCollider2D circleCollider2D;
    private GameObject wayPoint01;
    private GameObject wayPoint02;
    private GameObject wayPoint03;
    private GameObject wayPoint04;

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
        circleCollider2D = transform.GetComponent<CircleCollider2D>();
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
            DebugTeleport(1);
        if (Input.GetKeyDown(KeyCode.F2))
            DebugTeleport(2);
        if (Input.GetKeyDown(KeyCode.F3))
            DebugTeleport(3);
        if (Input.GetKeyDown(KeyCode.F4))
            DebugTeleport(4);
    }

    private void FixedUpdate()
    {
        if (horizontal * rigidbody.velocity.x < 0)
            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
        if (Mathf.Abs(rigidbody.velocity.x) < maxSpeed)
            rigidbody.AddForce(new Vector2(horizontal * speed, 0), ForceMode2D.Impulse);
        if (horizontal == 0 && canDecelerate)
        {
            rigidbody.velocity *= Vector2.up;
            //if (Mathf.Abs(rigidbody.velocity.x) < 0.1f)
            //rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
            // else
            //rigidbody.velocity = new Vector2(rigidbody.velocity.x / decelaration, rigidbody.velocity.y);
        }
        if (canJump && needJump)
        {
            jumpReleased = false;
            CancelInvoke("JumpInputCancel");
            needJump = false;
            StopVerticalVelocity();
            rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        if (transform.position.y < -150)
        {
            healthScript.Death();
        }
    }

    private void DebugTeleport(int id)
    {
        switch (id)
        {
            case 1:
                transform.position = wayPoint01.transform.position;
                break;
            case 2:
                transform.position = wayPoint02.transform.position;
                break;
            case 3:
                transform.position = wayPoint03.transform.position;
                break;
            case 4:
                transform.position = wayPoint04.transform.position;
                break;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        //Debug.Log("Enter");        
        if (collision.gameObject.CompareTag("Terrain"))
        {
            if (!HasCollidedWithGround(collision.contacts))
            {
                canJump = false;
                //BounceByWall(collision.GetContact(0));
            }            
        }
    }

    //private void BounceByWall(ContactPoint2D point)
    //{
    //    Vector2 direction = Vector2.Reflect(rigidbody.velocity.normalized, point.normal);
    //    rigidbody.AddForce(direction * bounceForce, ForceMode2D.Impulse);
    //}

    private bool HasCollidedWithGround(ContactPoint2D[] contacts)
    {
        Vector2 validDirection = Vector2.up;
        foreach (ContactPoint2D contactPoint in contacts)
        {
            if (Vector2.Angle(contactPoint.normal, validDirection) <= jumpAngleTreshold)
            {
                //Debug.Log("Ground: " + contactPoint);
                canDecelerate = true;
                canJump = true;
                //Debug.Log("CanJump");
                return true;
            }
            //Debug.Log("NotGround: " + contactPoint);
        }
        return false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            canDecelerate = false;
            canJump = false;
            //Debug.Log("Exit");
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{        
    //    if (collision.gameObject.CompareTag("Terrain"))
    //    {
    //        //canJump = false;
    //        onGround = true;
    //        //Debug.Log("Grounded: " + onGround);
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Terrain"))
    //    {
    //        onGround = false;
    //        //Debug.Log("Grounded: " + onGround);
    //    }
    //}

    //private bool Grounded()
    //{
    //    //RaycastHit2D raycast = Physics2D.CircleCast(circleCollider2D.bounds.center, circleCollider2D.radius, Vector2.down, groundTouchDistance, terrainMask);
    //    //return raycast.collider != null;
    //    return onGround;
    //}

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
