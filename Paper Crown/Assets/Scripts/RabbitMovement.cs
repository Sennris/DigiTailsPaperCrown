using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Tried to add a parabolic knockback effect, idk how tho, could you try?
// RFA - 02/10/2025

public class RabbitMovement : MonoBehaviour
{
    public static float groundSpeed;
    public GameObject player;
    private Rigidbody2D playerBody;
    public Rigidbody2D body;
    public float direction;
    public static float jumpSpeed;
    public float rayHitDistance;
    public float groundRayHitDistance;
    
    public LayerMask hitLayer;
    public LayerMask groundLayer;
    public GameObject obstacleRay;
    public GameObject groundRay;
    private RaycastHit2D hitObstacle;
    private RaycastHit2D groundHit;
    public int actualHitLayer;
    private int actualHitLayerMask;
    
    private bool canFlip = true;
    private Animator toyAnimator;
    private bool grounded;

    private float flipDelay = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        toyAnimator = GetComponent<Animator>();
        playerBody = player.GetComponent<Rigidbody2D>();
        groundSpeed = 1f;
        jumpSpeed = 5.5f;
        flipDelay = 0.5f;
    }
    
    void FixedUpdate()
    {
        if (toyAnimator.GetBool("PlayerHit"))
        {
            if (grounded == true)
            {
                toyAnimator.SetBool("PlayerHit", false);
            }
        }

        if (player.transform.position.x > transform.position.x && canFlip)
        {
            direction = 1;
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            StartCoroutine(DelayFlip());
        }

        else if (canFlip)
        {
            direction = -1;
            if (transform.localScale.x > 0)
            {
                transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
            }
            StartCoroutine(DelayFlip());
        }

        hitObstacle = Physics2D.Raycast(obstacleRay.transform.position, Vector2.right * new Vector2(direction, 0f), rayHitDistance, hitLayer);
        groundHit = Physics2D.Raycast(groundRay.transform.position, Vector2.down, groundRayHitDistance, groundLayer);
        if (hitObstacle)
        {
            actualHitLayer = hitObstacle.collider.gameObject.layer;
            actualHitLayerMask = 1 << actualHitLayer;
            if (actualHitLayerMask == hitLayer)
            {
                body.linearVelocity = new Vector2(body.linearVelocityX, jumpSpeed);
            }
        }
        
        if (groundHit)
        {
            actualHitLayer = groundHit.collider.gameObject.layer;
            actualHitLayerMask = 1 << actualHitLayer;
            if (actualHitLayerMask == groundLayer)
            {
                grounded = true;
            }
        }
        
        else
        {
            grounded = false;
        }
    }

    public void BunnyHop()
    {
        if (grounded)
        {
            body.AddForce(new Vector2(groundSpeed * direction, jumpSpeed), ForceMode2D.Impulse);
        }
    }

    private IEnumerator DelayFlip()
    {
        canFlip = false;
        yield return new WaitForSeconds(flipDelay);
        canFlip = true;
    }
}
