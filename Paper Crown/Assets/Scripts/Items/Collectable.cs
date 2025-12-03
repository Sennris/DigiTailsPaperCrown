using UnityEngine;

public class Collectible : MonoBehaviour
{
    public static bool hasKey;
    public static bool hasWindowKey;

    void Start()
    {
        hasKey = false;
        hasWindowKey = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object has the tag "Player"
        Debug.Log("collision detected");
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("WindowKey"))
            {
                hasWindowKey = true;
            }

            else
            {
                SoundManager.PlaySound("Key_Jingle");
                hasKey = true;
            }
            // Optional: play a sound or add score here
            // SoundManager.PlaySound(SoundType.KEYPICKUP);
            Destroy(gameObject); // remove key from scene
        }
    }
}