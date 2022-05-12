using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericObjectPooler : MonoBehaviour
{
    public GameObject pooledObject;
    List<GameObject> objectList;
        
    void Start()
    {
        objectList = new List<GameObject>();
        GameObject gameObject = Instantiate(pooledObject);
        gameObject.SetActive(false);
        objectList.Add(gameObject);
    }
    
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < objectList.Count; i++)
        {
            if(!objectList[i].activeInHierarchy)
            {
                return objectList[i];
            }
        }
        GameObject gameObject = Instantiate(pooledObject);
        objectList.Add(gameObject);
        return gameObject;
    }
}
