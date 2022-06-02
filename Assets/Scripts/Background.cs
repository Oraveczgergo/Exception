using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float xEffect;
    public float yEffect;
    public GameObject followedObject;
    private float length;
    private Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        float xDistance = followedObject.transform.position.x * xEffect; 
        float yDistance = followedObject.transform.position.y * yEffect;
        transform.position = new Vector3(startPos.x + xDistance, startPos.y + yDistance, transform.position.z);
    }
}
