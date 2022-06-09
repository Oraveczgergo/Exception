using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleCameraScript : MonoBehaviour
{
    private float speed = 10;
    private new Rigidbody2D rigidbody;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        
    }
    public void SyncSpeed(float speed)
    {
        this.speed = speed;
    }

    public void NormalMovement()
    {
        rigidbody.velocity = Vector2.zero;
        rigidbody.AddForce(new Vector2(speed, 0), ForceMode2D.Impulse);
    }

    public void FastMovement()
    {
        rigidbody.velocity = Vector2.zero;
        rigidbody.AddForce(new Vector2(speed * 1.5f, 0), ForceMode2D.Impulse);
    }

    public void SlowMovement()
    {
        rigidbody.velocity = Vector2.zero;
        rigidbody.AddForce(new Vector2(speed / 1.5f, 0), ForceMode2D.Impulse);
    }
}
