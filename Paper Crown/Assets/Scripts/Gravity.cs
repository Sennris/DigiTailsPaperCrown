using UnityEngine;

public class Gravity : MonoBehaviour
{
    public Rigidbody2D body;

    public void EnableGravity()
    {
        body.gravityScale = 3;

        PlayerMovement playerMovement = transform.parent.GetComponent<PlayerMovement>();
        playerMovement.EnableMovement();
    }
}
