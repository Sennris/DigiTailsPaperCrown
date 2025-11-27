using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Added a very very visually unappealing block of code that leads to the players escape and win
// RFA 14/10/2025

public class Escape : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public DisableToyMovement disableToyMovement;
    public Cutscene cutscene;
    public float airTime;

    public GameObject window;
    public StopParticles stopParticles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator StartEscapeSequence()
    {
        stopParticles.DisableParticles(window.gameObject);
        playerMovement.DisableMovement();
        disableToyMovement.DisableMovement();
        playerMovement.body.linearVelocity = new Vector2(0, playerMovement.jumpSpeed);
        playerMovement.playerAnimator.SetBool("isJumping", !playerMovement.grounded);
        yield return new WaitForSeconds(airTime);
        cutscene.StartCutscene("PlayerEscapeAnimatic");
    }
}
