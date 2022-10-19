using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject waypoint;
    public GameObject water;
    private WaterScript waterScript;
    private CharacterController2D characterController;

    private void Start()
    {
        waterScript = water.GetComponent<WaterScript>();
        characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.position = waypoint.transform.position;
            waterScript.WaterStart();
            characterController.CurrentSectionId = 3;
        }
    }
}
