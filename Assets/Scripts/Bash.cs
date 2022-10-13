using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bash : MonoBehaviour
{
    public float Range = 2f;
    RaycastHit2D[] objects;
    Vector3 direction;
    public float force = 1f;
    private bool needRotate = false;
    private bool objectFound = false;
    private float minDistance = float.MaxValue;
    private float currentDistance;
    private GameObject target;
    public Transform arrow;
    public float aimingTime = 2f;
    private float tempTime;
    private Rigidbody2D rb;
    void Start()
    {
         rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            objects = Physics2D.CircleCastAll(transform.position, Range, Vector3.forward);
            foreach (RaycastHit2D o in objects)
            {
                //Debug.Log(o.collider.gameObject.name);
                if (o.collider.gameObject.CompareTag("Bullet") || o.collider.gameObject.CompareTag("Bashable"))
                {
                    Time.timeScale = 0;
                    objectFound = true;
                    tempTime = Time.realtimeSinceStartup;
                    currentDistance = Vector3.Distance(o.collider.gameObject.transform.position, transform.position);
                    if (currentDistance < minDistance)
                    {
                        minDistance = currentDistance;
                        target = o.collider.gameObject;
                        if (target.CompareTag("Bullet"))
                            needRotate = true;
                        if (target.CompareTag("Bashable"))
                            needRotate = false;
                    }
                }
            }
            minDistance = float.MaxValue;
            if (objectFound)
            {
                arrow.gameObject.SetActive(true);
                arrow.position = target.transform.position;
                arrow.Translate(0, 0, 10);
            }
        }
        else if (Input.GetButtonUp("Fire2") && objectFound)
        {            
            Ability();
        }
        else if (Input.GetButton("Fire2") && objectFound)
        {
            if (Time.realtimeSinceStartup - tempTime > aimingTime)
            {
                Ability();
            }
            else
                RotateArrowToCursor(arrow);                      
        }

    }

    private void Ability()
    {
        objectFound = false;
        Time.timeScale = 1;
        arrow.gameObject.SetActive(false);
        direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        direction.z = 0;
        direction = direction.normalized;
        transform.position = target.transform.position + direction * 3;
        if (needRotate)
            RotateBashedObject(target.transform);
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;        
        rb.AddForce(direction * force, ForceMode2D.Impulse);      
    }

    private void RotateArrowToCursor(Transform o)
    {
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();
        float rotation_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        o.transform.rotation = Quaternion.Euler(0f, 0f, rotation_z - 90);
    }
    private void RotateBashedObject(Transform o)
    {
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();
        float rotation_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        o.transform.rotation = Quaternion.Euler(0f, 0f, rotation_z - 180);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
