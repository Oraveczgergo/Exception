using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAim : MonoBehaviour
{
    public GameObject target;
    void Start()
    {

    }

    void Update()
    {
        Vector3 direction = GetDirection();
        Debug.DrawLine(transform.position, target.transform.position);
        float zRotation = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        transform.Rotate(new Vector3(0, 0, -zRotation) - transform.rotation.eulerAngles);
    }

    private Vector3 GetDirection()
    {
        return (target.transform.position - transform.position).normalized;
    }
}
