using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;

public class PlayerInteract : MonoBehaviour
{
    public GameObject musicBox;
    public GameObject itemKey;
    public GameObject spawner;
    private bool canPlay = true;

    public StopParticles stopParticles;

    private float shakeLength = 0.5f;
    
    private int shakeTimes = 3;
    //private int interactablesCount = 1;
    int value = ObjectTextScript.touched;
    public Animator animator;

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E) && canPlay == true && Vector3.Distance(transform.position, musicBox.transform.position) < 2)
        {
            PopupTutorial.InteractE = true;
            animator.SetTrigger("Opened");
            canPlay = false;
            Instantiate(itemKey, spawner.transform.position, Quaternion.identity);
            SoundManager.PlaySound(SoundType.MUSICBOX_OPEN);
            SoundManager.DelayPlaySound(SoundType.MUSICBOX, 1);


            StartCoroutine(RepeatShake());
            stopParticles.DisableParticles(musicBox);
        }
    }

    public IEnumerator RepeatShake()
    {
        for (int i = 0; i < shakeTimes; i++)
        {
            SoundManager.PlaySound(SoundType.TOY_BANG);

            GetComponentInChildren<CinemachineImpulseSource>().GenerateImpulse();

            yield return new WaitForSeconds(shakeLength);
        }
    }
}
