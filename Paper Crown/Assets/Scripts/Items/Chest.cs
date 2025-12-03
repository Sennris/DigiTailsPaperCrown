using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine.Rendering.Universal;

public class Chest : MonoBehaviour
{
    //public GameObject chestClosed;
    //public GameObject chestOpen;

    private bool playerIsClose = false;
    public ToySpawn toySpawn;
    public Cutscene cutscene;
    public StopParticles stopParticles;

    private float shakeLength = 0.5f;
    private int shakeTimes = 3;
    public GameObject sword;
    public static bool canEscape;
    public bool hasSword = false;
    public Animator closetAnimator;

    public Animator playerAnimator;
    public Animator chestAnimator;

    public Light2D ambientLight;

    void Start()
    {
        //chestClosed.SetActive(true);
        //chestOpen.SetActive(false);
        //playerAnimator = playerAnimator.GetComponent<Animator>();
        canEscape = false;
    }

    void Update()
    {
        if (playerIsClose && Input.GetKeyDown(KeyCode.E))
        {
            if (Collectible.hasKey)
            {
                PopupTutorial.InteractE = true;
                ambientLight.intensity = 0.01f;

                // Open chest
                //chestClosed.SetActive(false);
                //chestOpen.SetActive(true);
                Collectible.hasKey = false;

                GivePlayerSword();
                playerAnimator.SetBool("hasSword", hasSword);

                canEscape = true;
                chestAnimator.SetTrigger("Opened");
                SoundManager.PlaySound("Toybox_Open", 0.6f);

                cutscene.StartCutscene("ToyEscapeAnimatic");
                //StartCoroutine(RepeatShake());
                closetAnimator.SetTrigger("Open");
                stopParticles.DisableParticles(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerIsClose = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerIsClose = false;
    }

    // unused
    public IEnumerator RepeatShake()
    {
        for (int i = 0; i < shakeTimes; i++)
        {
            SoundManager.PlaySound("Toy_Bang");

            GetComponentInChildren<CinemachineImpulseSource>().GenerateImpulse();
            yield return new WaitForSeconds(shakeLength);
        }
    }

    private void GivePlayerSword()
    {
        if (sword != null)
        {
            sword.SetActive(true);
            hasSword = true;
        }
    }
}
