using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Cutscene : MonoBehaviour
{
    public Animator cutsceneAnimator;
    public Animator creditsAnimator;
    public Animator playerAnimator;
    public ToySpawn toySpawn;
    public PlayerMovement playerMovement;
    public DisableToyMovement disableToyMovement;
    public GameObject restartButton;
    public GameObject[] Toys;
    public float delayTime;
    public Image cutsceneSprite;

    public Vector3 teleportPosition;
    public Transform player;



    public void StartCutscene(string NameOfCutscene)
    
    {
        if (NameOfCutscene == "ToyEscapeAnimatic")
        {
            StartCoroutine(DelayCutscene(NameOfCutscene));
        }

        else if (NameOfCutscene == "GameOverScreen")
        {
            playerAnimator.SetTrigger("isDead");
            playerMovement.DisableMovement();
            disableToyMovement.DisableMovement();
            StartCoroutine(DelayCutscene(NameOfCutscene));
        }

        else if (NameOfCutscene == "PlayerEscapeAnimatic")
        {
            playerMovement.DisableMovement();
            cutsceneAnimator.SetTrigger("PlayCutscene3");
            restartButton.SetActive(true);
        }

        else
        {
            Debug.Log("That is not an existing cutscene!");
        }
    }

    public void CallToySpawn()
    {
        toySpawn.SpawnToys();
    }

    IEnumerator DelayCutscene(string NameOfCutscene)
    {
        yield return new WaitForSeconds(delayTime);
        if (NameOfCutscene == "ToyEscapeAnimatic")
        {
            playerMovement.DisableMovement();
            cutsceneAnimator.SetTrigger("PlayCutscene1");
        }

        else if (NameOfCutscene == "GameOverScreen")
        {
            playerMovement.DisableMovement();
            cutsceneAnimator.SetTrigger("PlayCutscene2");
            restartButton.SetActive(true);
        }
    }

    public void RollCredits()
    {
        creditsAnimator.SetTrigger("RollCredits");
    }

    public void CallEnableMovement()
    {
        playerMovement.EnableMovement();
    }
}
