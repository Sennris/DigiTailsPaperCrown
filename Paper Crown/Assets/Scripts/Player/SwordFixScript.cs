using UnityEngine;

public class SwordFixScript : MonoBehaviour
{
    public PlayerMovement playerMovement;

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.isWalking)
        {
            transform.localPosition = new Vector2(0.14f, -0.132f);
        }

        else
        {
            transform.localPosition = new Vector2(0.057f, -0.122f);
        }
    }
}
