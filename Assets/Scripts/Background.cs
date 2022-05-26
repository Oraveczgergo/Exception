using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float xEffect;
    public float yEffect;
    public GameObject camera;
    private float length;
    private Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float xDistance = camera.transform.position.x * xEffect; 
        float yDistance = camera.transform.position.y * yEffect;
        transform.position = new Vector3(startPos.x + xDistance, startPos.y + yDistance, transform.position.z);
    }
}
