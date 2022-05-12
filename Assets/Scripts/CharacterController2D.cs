using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController2D : MonoBehaviour
{
    public float movementSpeedInput = 30;
    public float jumpForceInput = 60;
    private float movementSpeed;
    private float jumpForce;
    private HealthScript healthScript;
    private new Rigidbody2D rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        movementSpeed = movementSpeedInput;
        jumpForce = jumpForceInput;
        healthScript = (HealthScript)GetComponent("HealthScript");
    }

    private void Update()
    {
        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * movementSpeed;
        if (Input.GetButtonDown("Jump") && Mathf.Abs(rigidbody.velocity.y) < 0.001f)
        {
            rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        if (Input.GetButtonUp("Jump") && rigidbody.velocity.y > 0)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y / 2);
        }
        if(transform.position.y < -150)
        {            
            healthScript.TakeDamage(999);
        }
    }

    public void DoubleSpeed()
    {
        movementSpeed = movementSpeedInput * 2;
        Invoke("SpeedReset", 10f);
    }

    public void DoubleJumpForce()
    {
        jumpForce = jumpForceInput * 2;
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
