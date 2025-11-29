using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public Rigidbody rb;
    public SpriteRenderer sr;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(x, 0, y);
        rb.linearVelocity = moveDirection * speed;

        if (x < 0)
        {
            sr.flipX = true;
        }
        if (x > 0)
        {
            sr.flipX = false;
        }
    }
}
