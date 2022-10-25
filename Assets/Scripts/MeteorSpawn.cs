using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawn : MonoBehaviour
{

    public float spawnRate = 2f;
    private GenericObjectPooler meteorObjectPooler;
    private GenericObjectPooler bigMeteorObjectPooler;
    private GameObject Target;
    private bool isActive = false;
    void Start()
    {
        meteorObjectPooler = (GenericObjectPooler)GameObject.FindGameObjectWithTag("OP_Meteor").GetComponent("GenericObjectPooler");
        bigMeteorObjectPooler = (GenericObjectPooler)GameObject.FindGameObjectWithTag("OP_BigMeteor").GetComponent("GenericObjectPooler");
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
        GameObject meteor;
        if (Random.Range(0,4) > 0)
        {
            meteor = meteorObjectPooler.GetPooledObject();
        }
        else
        {
            meteor = bigMeteorObjectPooler.GetPooledObject();
        }
        if (gameObject == null) return;
        meteor.transform.position = new Vector3(Target.transform.position.x + Random.Range(-20, 90), Target.transform.position.y +100);
        meteor.transform.rotation = Quaternion.Euler(0f, 0f, meteor.transform.rotation.y + Random.Range(-120, -30));
        meteor.SetActive(true);
    }
}
