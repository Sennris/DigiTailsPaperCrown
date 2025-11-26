using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ObjectTextScript : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public string[] dialogue;
    private int index;

    //public GameObject contButton;
    public PlayerMovement player;

    public float wordSpeed;
    public bool playerIsClose;
    static bool alreadyPlaying;

    public bool beenTouched = false;
    public static int touched;
    public TMPro.TextMeshProUGUI touchCounterText;

    public bool importantItem;

    public Escape escape;
    public bool isEscape;

    public StopParticles stopParticles;

    void Start()
    {
        touchCounterText.text = touched.ToString();
        touched = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose && !alreadyPlaying)
        {
            PopupTutorial.InteractE = true;
            if (!importantItem)
            {
                if (!beenTouched)
                {
                    beenTouched = true;
                    touched += 1;
                    touchCounterText.text = touched.ToString();
                    if (stopParticles != null)
                    {
                        stopParticles.DisableParticles(gameObject);
                    }
                }
            }

            alreadyPlaying = true;
            if (Chest.canEscape && player.grounded && isEscape)
            {
                //Debug.Log(Chest.canEscape);
                zeroText();
                if (Chest.canEscape && player.grounded)
                {
                    StartCoroutine(escape.StartEscapeSequence());
                }
            }

            else
            {
                if (gameObject.name == "ClosedChest" && Collectible.hasKey)
                {
                    //Don't start typing
                    zeroText();
                }
                
                else
                {
                    dialoguePanel.SetActive(true);
                    StartCoroutine(Typing());
                }
            }

            if (gameObject.name == "ClosedChest" && !Collectible.hasKey)
            {
                SoundManager.PlaySound(SoundType.TOYBOX_LOCKED,0.5f);
            }
        }

        /* if (dialogueText.text == dialogue[index])
        {
            contButton.SetActive(true);
        } */
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
        yield return new WaitForSeconds(1f); // so ppl can read

        zeroText(); // testing no continue
    }

    /* public void NextLine()
    {

        contButton.SetActive(false);

        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    } */

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
        alreadyPlaying = false;
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
            //zeroText();
        }
    }
}
