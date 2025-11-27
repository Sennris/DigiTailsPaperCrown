using UnityEngine;

public class PopupTutorial : MonoBehaviour
{
    public GameObject tutorialPopup;
    public static bool InteractE;
    public static bool InteractK;
    public static bool InteractPush;
    
    public Chest chest;
    
    private bool playerIsClose;

    void Start()
    {
        InteractE = false;
        InteractK = false;
        InteractPush = false;
    }

    void Update()
    {
        if (playerIsClose)
        {
            if (gameObject.CompareTag("TutorialE") && !InteractE)
            {
                tutorialPopup.SetActive(true);
            }

            else if (gameObject.CompareTag("TutorialK") && !InteractK && chest.hasSword)
            {
                tutorialPopup.SetActive(true);
            }

            else if (gameObject.CompareTag("TutorialPush") && !InteractPush)
            {
                tutorialPopup.SetActive(true);
            }

            else
            {
                tutorialPopup.SetActive(false);
            }
        }

        else
        {
           tutorialPopup.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsClose = false;
        }
    }
}
