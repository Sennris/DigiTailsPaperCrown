using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D body;
    public BoxCollider2D groundCheck;

    public float jumpSpeed;
    public float groundSpeed;
    [Range(0f, 1f)]
    public float groundDecay;
    public LayerMask groundMask;
    public bool grounded;
    public bool isCrouching;
    public float direction;
    public Push push;
    public bool isWalking;
    public bool isJumping;

    public Animator playerAnimator;
    public Animator movementPopdownAnimator;

    public Knockback knockback;

    float xInput;
    float yInput;

    public bool controlsEnabled = true; // this will allow us to disable player movement


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DisableMovement();
        body.gravityScale = 0;

        direction = 1;
        playerAnimator = playerAnimator.GetComponent<Animator>();
        movementPopdownAnimator.SetTrigger("Popdown");
    }

    void FixedUpdate()
    {
        CheckGround();
        playerAnimator.SetFloat("xVelocity", Mathf.Abs(body.linearVelocity.x));
        playerAnimator.SetFloat("yVelocity", body.linearVelocity.y);
        //ApplyFriction();
        if (controlsEnabled)
        {
            isWalking = Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.D);
            playerAnimator.SetBool("isWalking", isWalking);
            GetInput();
            MoveWithInput();
            if (xInput == 0 && grounded && knockback.KBCounter <= 0)
            {
                body.linearVelocity = new Vector2(0, body.linearVelocity.y);
            }
        }
        
        else
        {
            body.linearVelocity = new Vector2(0, body.linearVelocity.y);
            playerAnimator.SetBool("isWalking", false);
        }
    }


    void CheckGround() // sees if player is grounded
    {
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
        // if (grounded)
        // {
        //     isJumping = false;
        //     playerAnimator.SetBool("isJumping", false);
        // }
        // else
        // {
        //     isJumping = true;
        //     playerAnimator.SetBool("isJumping", true);
        // }
    }

    void MoveWithInput()
    {
        // A and D movement
        if(Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.D))
        {
            body.linearVelocity = new Vector2(xInput * groundSpeed, body.linearVelocity.y);
            direction = Mathf.Sign(xInput);
        }

        // Jumping
        if (Input.GetKey(KeyCode.Space) && grounded && !push.isPushing && !isCrouching)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpSpeed);
            grounded = false;
            playerAnimator.SetBool("isJumping", !grounded);
        }



        // crouch button
        if (Input.GetKey(KeyCode.LeftShift) && push.isPushing == false)
        {
            if (grounded)
            {
                isCrouching = true; // player is now crouching
                playerAnimator.SetBool("isCrouching", true);
                transform.localScale = new Vector3(direction * 2, 2, 2);
                //player cant jump
            }
        }
        else if(push.isPushing == false)// if not crouching set back to normal
        {
            isCrouching = false;
            playerAnimator.SetBool("isCrouching", false);
            transform.localScale = new Vector3(direction * 2, 2, 2);
        }
        // -- end of crouch
    }

    void GetInput()
    {
        xInput = Input.GetAxis("Horizontal");
    }

    //void ApplyFriction()
    //{
    //    if (transform.localScale.y.magnitude <= -1)

    //    if (grounded && xInput == 0 && yInput == 0)
    //    {
    //        body.linearVelocity *= groundDecay;
    //    }
    //    if (xInput == 0 && grounded)
    //    {
    //        body.linearVelocity = new Vector2(0, body.linearVelocity.y);
    //    }
    //}
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 7)
        {
            SoundManager.PlaySound("Player_Jump");
            isJumping = false;
            playerAnimator.SetBool("isJumping", false);
        }
    }

    public void DisableMovement()
    {
        body.linearVelocity = new Vector2(0, 0);
        controlsEnabled = false;
    }

    public void EnableMovement()
    {
        controlsEnabled = true;
    }
}