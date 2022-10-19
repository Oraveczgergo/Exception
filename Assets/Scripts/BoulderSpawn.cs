using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderSpawn : MonoBehaviour
{
    private GenericObjectPooler objectPooler;
    public float spawnDelay = 5f;

    private void Start()
    {
        objectPooler = (GenericObjectPooler)GameObject.FindGameObjectWithTag("OP_Boulder").GetComponent("GenericObjectPooler");
        InvokeRepeating("SpawnBoulder", 5f, 5f);
    }

    private void SpawnBoulder()
    {
        GameObject boulder = objectPooler.GetPooledObject();
        boulder.transform.position = transform.position;
        boulder.SetActive(true);       
    }
}
