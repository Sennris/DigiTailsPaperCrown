using UnityEngine;

public class DisableToyMovement : MonoBehaviour
{
    public void DisableMovement()
    {
        WalkToyMovement.movementEnabled = false;
        RabbitMovement.jumpSpeed = 0;
        RabbitMovement.groundSpeed = 0;
    }
}
