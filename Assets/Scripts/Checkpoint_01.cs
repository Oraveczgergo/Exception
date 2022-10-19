using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint_01 : MonoBehaviour
{
    CharacterController2D characterController;
    void Start()
    {
        characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            characterController.CurrentSectionId = 2;
        }
    }
}
