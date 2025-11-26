using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkToyMovement : MonoBehaviour
{
    private float direction;
    private Rigidbody2D body;
    private bool canFlip = true;
    
    private Rigidbody2D playerBody;
    public GameObject player;

    public float groundSpeed;
    public float jumpSpeed;
    public float flipDelay;

    private RaycastHit2D hitObstacle;
    public GameObject obstacleRay;
    public float rayHitDistance;
    public LayerMask hitLayer;
    private int actualHitLayer;
    private int actualHitLayerMask;

    public float groundRayHitDistance;
    public LayerMask groundLayer;
    public GameObject groundRay;
    private RaycastHit2D groundHit;
    private bool grounded;

    public static bool movementEnabled;

    private float randomNum;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        playerBody = player.GetComponent<Rigidbody2D>();
        movementEnabled = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (movementEnabled)
        {
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

            if (grounded)
            {
                body.linearVelocity = new Vector2(groundSpeed * direction, body.linearVelocity.y);
            }

            hitObstacle = Physics2D.Raycast(obstacleRay.transform.position, Vector2.right * new Vector2(direction, 0f), rayHitDistance, hitLayer);
            if (hitObstacle)
            {
                actualHitLayer = hitObstacle.collider.gameObject.layer;
                actualHitLayerMask = 1 << actualHitLayer;
                if (actualHitLayerMask == hitLayer)
                {
                    body.linearVelocity = new Vector2(body.linearVelocityX, jumpSpeed);
                }
            }
            
            groundHit = Physics2D.Raycast(groundRay.transform.position, Vector2.down, groundRayHitDistance, groundLayer);
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
    }

    private IEnumerator DelayFlip()
    {
        canFlip = false;
        yield return new WaitForSeconds(flipDelay);
        canFlip = true;
    }
}
