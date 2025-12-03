using UnityEngine;

public class PlaySounds : MonoBehaviour
{
    public void PlayFootstepSound()
    {
        SoundManager.PlaySound("Player_Footsteps", 0.2f);
    }

    public void PlayPushSound()
    {
        SoundManager.PlaySound("Push", 0.3f);
    }

    public void PlaySneakSound()
    {
        SoundManager.PlaySound("Player_Sneak");
    }

    public void PlayClosetOpenSound()
    {
        SoundManager.PlaySound("Closet_Creak", 2);
    }
}
