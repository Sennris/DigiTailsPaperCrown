using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public PauseMenu pauseMenu;
    
    public void StartNewGame()
    {
        SceneManager.LoadSceneAsync(1);
        SoundManager.PlaySound("Scribble");
    }

    public void OpenSettings()
    {
        pauseMenu.Pause();
        SoundManager.PlaySound("Scribble");
    }

    public void CloseSettings()
    {
        pauseMenu.Resume();
        SoundManager.PlaySound("Scribble");
    }
}
