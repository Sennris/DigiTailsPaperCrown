using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;

public class Attack : MonoBehaviour
{
    public float attackCooldown = 0.5f; // Cooldown duration in seconds
    private float lastAttackTime = 0f;
    private Lantern lantern;
    public Chest chest;

    public Animator playerAnimator;

    void Update()
    {
        // Check if left mouse button is pressed and cooldown has passed
        if (Input.GetKeyDown(KeyCode.K) && Time.time >= lastAttackTime + attackCooldown)
        {
            AttackAction();
            lastAttackTime = Time.time; // Reset the cooldown
        }
    }

    void AttackAction()
    {
        if (chest.hasSword)
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 2);
            playerAnimator.SetTrigger("isAttacking");
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Toy"))
                {
                    SoundManager.PlaySound("Player_Hurt");
                    SoundManager.PlaySound("Toy_Hurt");
                    //GetComponentInChildren<CinemachineImpulseSource>().GenerateImpulse();

                    var toyHealth = hitCollider.GetComponentInParent<ToyHealth>();
                    if (toyHealth != null)
                        toyHealth.Damage();

                    var knockback = hitCollider.GetComponentInParent<Knockback>();
                    if (knockback != null)
                        knockback.DealKnockback(hitCollider.gameObject, gameObject);

                    var toyAnimator = hitCollider.GetComponentInParent<Animator>();

                    if (toyAnimator != null)
                    {
                        toyAnimator.SetBool("PlayerHit", true);
                    }
                }

                if (hitCollider.CompareTag("Lantern"))
                {
                    PopupTutorial.InteractK = true;
                    lantern = hitCollider.GetComponentInChildren<Lantern>();
                    if (!lantern.currentlyLit && !lantern.hasBeenLitBefore)
                    {
                        StartCoroutine(lantern.Activate());
                    }
                }
            }
        }
    }
}