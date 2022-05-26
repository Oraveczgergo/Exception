using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController2D : MonoBehaviour
{
    public float movementSpeedInput = 30;
    public float jumpForceInput = 60;
    public float maxSpeed = 30f;
    public float decelaration = 10f;
    private float movementSpeed;
    private float jumpForce;
    private HealthScript healthScript;
    private new Rigidbody2D rigidbody;
    private bool jumping = false;
    private bool needJump = false;
    private float movement;

    private void Start()
    {        rigidbody = GetComponent<Rigidbody2D>();
        
        healthScript = (HealthScript)GetComponent("HealthScript");
    }
    private void Update()
    {
        movement = Input.GetAxis("Horizontal");
        if (!jumping && Input.GetKeyDown(KeyCode.Space))
        {
            needJump = true;
        }
    }

    private void FixedUpdate()
    {
        movementSpeed = movementSpeedInput;
        jumpForce = jumpForceInput;
        if (movement * rigidbody.velocity.x < 0)
            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
        if (Mathf.Abs(rigidbody.velocity.x) < maxSpeed)
            rigidbody.AddForce(new Vector2(movement * movementSpeed, 0), ForceMode2D.Impulse);
        if (movement == 0)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x / decelaration, rigidbody.velocity.y);
        }
        //rigidbody.MovePosition(rigidbody.position + new Vector2(movement, 0) * Time.deltaTime * movementSpeed);
        //transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * movementSpeed;
        if (needJump)
        {
            needJump = false;
            jumping = true;
            rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        if (Input.GetKeyUp(KeyCode.Space) && rigidbody.velocity.y > 0)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y / 2);
        }
        if(transform.position.y < -150)
        {            
            healthScript.Death();
        }
        if(Mathf.Abs(rigidbody.velocity.y) < 0.003f)
        {
            jumping = false;
        }
    }

    public void DoubleSpeed() //TODO: Megoldani boollal, Inputoktól megszabadulni
    {
        movementSpeed = movementSpeedInput * 2;
        Invoke("SpeedReset", 10f);
    }

    public void ExtraJumpForce()
    {
        jumpForce = jumpForceInput * 1.5f;
        Invoke("JumpReset", 10f);
    }

    private void SpeedReset()
    {
        movementSpeed = movementSpeedInput;
    }

    private void JumpReset()
    {
        jumpForce = jumpForceInput;
    }
}
