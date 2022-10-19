using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBooster : MonoBehaviour
{
    private CharacterController2D characterController;
    private void Start()
    {
        characterController = (CharacterController2D)GameObject.FindGameObjectWithTag("Player").GetComponent("CharacterController2D");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
            characterController.DoubleSpeed();
            Invoke("Reactivate", 10f);
        }
    }
    private void Reactivate()
    {
        gameObject.SetActive(true);
    }

    public void ForceRespawn()
    {
        CancelInvoke();
        gameObject.SetActive(true);
    }
}
