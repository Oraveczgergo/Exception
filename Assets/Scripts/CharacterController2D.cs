using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private LayerMask terrainMask;
    public float maxSpeed = 30f;
    public float decelaration = 10f;
    public float movementSpeed = 30f;
    public float jumpForce = 60f;
    private HealthScript healthScript;
    private new Rigidbody2D rigidbody;
    private bool jumping = false;
    private bool needJump = false;
    private bool jumpBoost = false;
    private bool speedBoost = false;
    private float movement;
    private BoxCollider2D boxCollider2D;

    private void Start()
    {        
        rigidbody = GetComponent<Rigidbody2D>();        
        healthScript = (HealthScript)GetComponent("HealthScript");
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        movement = Input.GetAxis("Horizontal");
        if (!jumping && Input.GetKeyDown(KeyCode.Space))
        {
            needJump = true;
            Invoke("JumpInputCancel", .2f);
        }
    }

    private void FixedUpdate()
    {
        if (movement * rigidbody.velocity.x < 0)
            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
        if (Mathf.Abs(rigidbody.velocity.x) < (speedBoost ? maxSpeed * 1.5f : maxSpeed))
            rigidbody.AddForce(new Vector2(movement * (speedBoost ? movementSpeed * 1.5f : movementSpeed), 0), ForceMode2D.Impulse);
        if (movement == 0)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x / decelaration, rigidbody.velocity.y);
        }
        if (Grounded() && needJump)
        {
            CancelInvoke("JumpInputCancel");
            needJump = false;
            jumping = true;
            rigidbody.AddForce(new Vector2(0, (jumpBoost ? jumpForce * 1.5f : jumpForce)), ForceMode2D.Impulse);
        }
        if (Input.GetKeyUp(KeyCode.Space) && rigidbody.velocity.y > 0)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y / 2);
        }
        if(transform.position.y < -150)
        {            
            healthScript.Death();
        }
        //if(Mathf.Abs(rigidbody.velocity.y) < 0.003f)
        if (Grounded())
        {
            jumping = false;
        }
    }

    private bool Grounded()
    {
        RaycastHit2D raycast = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .1f, terrainMask);
        return raycast.collider != null;
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
