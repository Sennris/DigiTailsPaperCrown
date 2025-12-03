using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Physics Components")]
    public Rigidbody2D body;
    public BoxCollider2D groundCheck;
    public Knockback knockback;
    public Push push;

    [Header("Animators")]
    public Animator playerAnimator;
    public Animator movementPopdownAnimator;

    [Header("Player States")]
    public bool isCrouching;
    public bool isWalking;
    public bool isJumping;

    [Header("Speeds")]
    public float jumpSpeed;
    public float groundSpeed;

    [Header("Ground Detection")]
    public bool grounded;
    public float groundDecay;
    public LayerMask groundMask;

    [Header("Player Control Variables")]
    float xInput;
    public float direction;
    public bool controlsEnabled = true; // This will allow us to disable player movement


    void Start()
    {
        // Remove movement and gravity upon starting
        DisableMovement();
        body.gravityScale = 0;

        // Set up playerAnimator and movementPopdownAnimator
        playerAnimator = playerAnimator.GetComponent<Animator>();
        movementPopdownAnimator.SetTrigger("Popdown");
    }


    void FixedUpdate()
    {
        // Check is player is on the ground and update animator velocities
        CheckGround();
        playerAnimator.SetFloat("xVelocity", Mathf.Abs(body.linearVelocity.x));
        playerAnimator.SetFloat("yVelocity", body.linearVelocity.y);
        
        // If the controls are enabled, move the player
        if (controlsEnabled)
        {
            MovePlayer();
            if (!isWalking && grounded && knockback.KBCounter <= 0)
            {
                body.linearVelocity = new Vector2(0, body.linearVelocity.y);
            }
        }

        // Otherwise, stop the player's horizontal movement
        else
        {
            body.linearVelocity = new Vector2(0, body.linearVelocity.y);
            isWalking = false;
        }
        
        // Update isWalking for the player animator
        playerAnimator.SetBool("isWalking", isWalking);
    }
    
    
    // Enable and disable player movement
    public void EnableMovement()
    {
        controlsEnabled = true;
    }

    public void DisableMovement()
    {
        body.linearVelocity = new Vector2(0, 0);
        controlsEnabled = false;
    }


    // Determines is the player is on the ground
    void CheckGround()
    {
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
    }


    // Get the player's input, and update isWalking appropriately
    void GetInput()
    {
        xInput = Input.GetAxis("Horizontal");

        if (xInput != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }


    void MovePlayer()
    {
        // Horizontal Movement
        GetInput();
        body.linearVelocity = new Vector2(xInput * groundSpeed, body.linearVelocity.y);

        // Jumping
        if (Input.GetKey(KeyCode.Space) && grounded && !push.isPushing && !isCrouching)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpSpeed);
            grounded = false;
            playerAnimator.SetBool("isJumping", !grounded);
        }

        // Crouching Logic
        if (Input.GetKey(KeyCode.LeftShift) && push.isPushing == false)
        {
            if (grounded)
            {
                // The player is now crouching
                isCrouching = true;
                playerAnimator.SetBool("isCrouching", true);
                transform.localScale = new Vector3(direction * 2, 2, 2);
                // The player can't jump
            }
        }
        else if(push.isPushing == false)
        {
            // If not crouching set back to normal
            isCrouching = false;
            playerAnimator.SetBool("isCrouching", false);
            transform.localScale = new Vector3(direction * 2, 2, 2);
        }
    }
    

    // This triggers when the players collides with something on the collsion layer 7
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 7)
        {
            SoundManager.PlaySound("Player_Jump");
            isJumping = false;
            playerAnimator.SetBool("isJumping", false);
        }
    }
}