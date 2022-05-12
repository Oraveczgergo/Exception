using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour
{
    public float fireRate = 0.5f;
    private GenericObjectPooler objectPooler; 
    void Start()
    {
        objectPooler = (GenericObjectPooler)GameObject.FindGameObjectWithTag("OP_Bullet").GetComponent("GenericObjectPooler");
        InvokeRepeating("Fire", fireRate, fireRate);
    }
    
    void Fire()
    {
        GameObject gameObject = objectPooler.GetPooledObject();
        if (gameObject == null) return;
        gameObject.transform.rotation = transform.rotation;
        gameObject.transform.position = transform.position;
        gameObject.SetActive(true);
    }
}
