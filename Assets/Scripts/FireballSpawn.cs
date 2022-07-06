using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpawn : MonoBehaviour
{

    public float spawnRate = 1f;
    private GenericObjectPooler objectPooler;
    private GameObject Target;
    private bool isActive = false;
    void Start()
    {
        objectPooler = (GenericObjectPooler)GameObject.FindGameObjectWithTag("OP_Fireball").GetComponent("GenericObjectPooler");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Vehicle" && !isActive)
        {
            isActive = true;
            Target = collision.gameObject;
            InvokeRepeating("Spawn", spawnRate, spawnRate);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Vehicle" && isActive)
        {
            isActive = false;
            CancelInvoke("Spawn");
        }        
    }

    void Spawn()
    {
        GameObject gameObject = objectPooler.GetPooledObject();
        if (gameObject == null) return;
        gameObject.transform.position = new Vector3(Target.transform.position.x +150, Target.transform.position.y + Random.Range(-20, 20));
        gameObject.SetActive(true);
    }
}
