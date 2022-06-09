using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float xEffect;
    public float yEffect;
    public GameObject followedObject;
    private float length;
    private Vector2 startPos;
    void Start()
    {
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        GameObject bg01 = GameObject.Find("BG_01");
        if (name == "BG_02")
        {
            transform.position = bg01.transform.position + Vector3.right * length;
        }
        else if (name == "BG_03")
        {
            transform.position = bg01.transform.position + Vector3.right * length * 2;
        }
        startPos = transform.position;
    }

    private void FixedUpdate()
    {
        float xDistance = followedObject.transform.position.x * xEffect;
        float yDistance = followedObject.transform.position.y * yEffect;
        float diffx = followedObject.transform.position.x - (startPos.x + xDistance);
        //if (name == "BG_01")           
            //Debug.Log(followedObject.name + " : " + diffx + " == " + (followedObject.transform.position.x - (startPos.x + xDistance)) + " ("+ followedObject.transform.position.x + " - " + (startPos.x + xDistance) + ")" + " length: " + length * 1.5f);
        if (diffx > length * 1.5f)
        {
            startPos += new Vector2(length * 3, 0);
        }
        transform.position = new Vector2(startPos.x + xDistance, startPos.y + yDistance);
    }
}
