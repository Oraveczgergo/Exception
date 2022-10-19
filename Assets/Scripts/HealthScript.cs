using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthScript : MonoBehaviour
{
    public int maxHealth = 20;
    public int currentHealth = 20;
    public TMPro.TMP_Text healthText;
    public float invulnerabilityAfterHit = 0.5f;
    private float damageTime = 0;
    public float spikeKnockback = 20f;
    private Rigidbody2D rigidBody;
    private CharacterController2D characterController;
    private GameObject player;

    private void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        characterController = player.GetComponent<CharacterController2D>();
    }

    public void TakeDamage(int x)
    {
        
        if(Time.fixedTime - damageTime > invulnerabilityAfterHit)
        {
            currentHealth -= x;
            damageTime = Time.fixedTime;
        }        
        if (currentHealth <= 0)
            Death();
    }

    public void TakeSpikeDamage(int x)
    {
        if (Time.fixedTime - damageTime > invulnerabilityAfterHit)
        {
            currentHealth -= x;
            damageTime = Time.fixedTime;
            rigidBody.velocity *= Vector2.right;
            rigidBody.AddForce(new Vector2(0, spikeKnockback), ForceMode2D.Impulse);
        }
        if (currentHealth <= 0)
            Death();
    }
    public void Heal(int x)
    {
        currentHealth += x;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }
    public void Death()
    {        
        characterController.RespawnPlayer();
    }

    void LateUpdate()
    {
        healthText.text = "Health: " + currentHealth.ToString();
    }
}
