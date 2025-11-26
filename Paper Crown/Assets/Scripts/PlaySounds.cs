using UnityEngine;

public class PlaySounds : MonoBehaviour
{
    public void PlayFootstepSound()
    {
        SoundManager.PlaySound(SoundType.PLAYER_FOOTSTEPS, 0.2f);
    }

    public void PlayPushSound()
    {
        SoundManager.PlaySound(SoundType.PUSH, 0.3f);
    }

    public void PlaySneakSound()
    {
        SoundManager.PlaySound(SoundType.PLAYER_SNEAK);
    }

    public void PlayClosetOpenSound()
    {
        SoundManager.PlaySound(SoundType.CLOSET_CREAK, 2);
    }
}
