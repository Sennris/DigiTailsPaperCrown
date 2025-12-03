using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyAttack : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Knockback knockback;
    public Animator playerAnimator;

    public float attackCooldown;
    private float nextActionTime;
    public LightControl playerLight; // Reference to LightControl script

    public bool canAttack = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && canAttack && playerHealth.health > 0)
        {
            playerAnimator.SetTrigger("isHit");
            // Shrink the player's light
            if (playerLight != null)
            {
                playerLight.TakeDamage();
            }


            SoundManager.PlaySound("Player_Hurt");


            playerHealth.health -= 1;
            playerHealth.healthCounterText.text = playerHealth.health.ToString();
            knockback.DealKnockback(collision.gameObject, gameObject);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
      
    }
}
