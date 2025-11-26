using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public PauseMenu pauseMenu;
    
    public void StartNewGame()
    {
        SceneManager.LoadSceneAsync(1);
        SoundManager.PlaySound(SoundType.SCRIBBLE);
    }

    public void OpenSettings()
    {
        pauseMenu.Pause();
        SoundManager.PlaySound(SoundType.SCRIBBLE);
    }

    public void CloseSettings()
    {
        pauseMenu.Resume();
        SoundManager.PlaySound(SoundType.SCRIBBLE);
    }
}
