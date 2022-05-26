using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    
    public GameObject followedObject;

    void LateUpdate()
    {
        Vector3 v = new Vector3(followedObject.transform.position.x, followedObject.transform.position.y, transform.position.z);
        transform.position = v;
    }
}
