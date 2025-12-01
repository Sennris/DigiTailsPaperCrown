using UnityEngine;

// Added jumping and ground checking
// RFA 1/12/2025

public class Movement : MonoBehaviour
{
    public float speed;
    public float jumpHeight;
    public Rigidbody rb;
    public SpriteRenderer sr;
    public bool grounded;

    public CapsuleCollider groundCheck;
    public LayerMask groundMask;
    public Collider[] myList;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        myList = Physics.OverlapSphere(groundCheck.transform.position, groundCheck.radius, groundMask);
        // If the list has any ground layer items in it, the player is grounded
        grounded = myList.Length > 0;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(x * speed, rb.linearVelocity.y, z * speed);
        rb.linearVelocity = moveDirection;
        
        // Character flipping
        if (x < 0)
        {
            sr.flipX = true;
        }
        if (x > 0)
        {
            sr.flipX = false;
        }
        
        // Jumping
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpHeight, rb.linearVelocity.z);
        }
    }
}
