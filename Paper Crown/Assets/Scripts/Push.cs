using UnityEngine;

public class Push : MonoBehaviour
{
    public GameObject pushSquare;
    public GameObject pushSquareParent;
    public bool isPushing;
    public PlayerMovement playerMovement;

    public Animator pushAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isPushing = false;
    }

    void Update()
    {
        pushAnimator.SetBool("isPushing", isPushing);
        if(Input.GetKey(KeyCode.J) && Vector3.Distance(transform.position, pushSquare.transform.position) < 1 && playerMovement.grounded == true && playerMovement.isCrouching == false)
        {
            PopupTutorial.InteractPush = true;
            isPushing = true;
            pushSquareParent.transform.parent = this.transform;
        }
        
        else
        {
            pushSquareParent.transform.parent = null;
            isPushing = false;
            pushSquare.transform.position = new Vector2(pushSquare.transform.position.x, -0.591f);
        }
    }
}