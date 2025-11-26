using UnityEngine;

public class Knockback : MonoBehaviour
{
    public Vector2 knockbackForce = new Vector2(10f, 10f);
    private Rigidbody2D body;
    public float KBCounter;
    public float KBForce;
    public float KBTotalTime;
    public bool KnockFromRight;

    public float knockbackGravityMultiplier = 3f; // Gravity multiplier during knockback
    private float originalGravity;

    public PlayerHealth playerHealth;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (KBCounter > 0 && playerHealth.health > 0)

        {
            if (KnockFromRight == true)
            {
                body.linearVelocity = new Vector2(-KBForce, KBForce);
                //body.AddForce(new Vector2(-KBForce, KBForce), ForceMode2D.Impulse);
            }

            if (KnockFromRight == false)
            {
                body.linearVelocity = new Vector2(KBForce, KBForce);
                //body.AddForce(new Vector2(KBForce, KBForce),ForceMode2D.Impulse);
            }
            
            KBCounter -= Time.deltaTime;
        }
    }

    public void DealKnockback(GameObject knockbackRecipient, GameObject knockbackDealer)
    {
        KBCounter = KBTotalTime;

        if (knockbackRecipient.transform.position.x < knockbackDealer.transform.position.x)
        {
            KnockFromRight = true;
        }

        else
        {
            KnockFromRight = false;
        }
    }
}
