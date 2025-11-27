using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lantern : MonoBehaviour
{
    private Knockback knockback;
    public float activateTime;
    public float activateIncrement;
    private float activateIncrementTime;
    public Light2D swordLight;
    private Light2D myLight;
    public bool currentlyLit;
    public bool hasBeenLitBefore;
    public float dimRate;
    public float totalRadius;
    public GameObject lanternSprite;

    public StopParticles stopParticles;
    public Animator animator;

    void Start()
    {
        myLight = GetComponent<Light2D>();
        myLight.intensity = 0;
        currentlyLit = false;
        hasBeenLitBefore = false;
        activateIncrementTime = totalRadius / (activateTime / activateIncrement);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Toy") && currentlyLit)
        {
            knockback = collision.gameObject.GetComponentInParent<Knockback>();
            knockback.DealKnockback(collision.gameObject, gameObject);
        }
    }

    public IEnumerator Activate()
    {
        SoundManager.PlaySound(SoundType.LANTERN_JINGLE, 0.3f);
        animator.SetTrigger("LanternOn");
        stopParticles.DisableParticles(lanternSprite.gameObject);
        myLight.intensity = .5f;
        swordLight.intensity = 0.5f;
        transform.localScale = new Vector2(0, 0);
        var scaleCopy = transform.localScale;
        currentlyLit = true;
        transform.localScale = new Vector2(scaleCopy.x += activateIncrement, scaleCopy.y += activateIncrement);
        while (myLight.transform.localScale.x < totalRadius)
        {
            myLight.transform.localScale = new Vector2(scaleCopy.x += activateIncrement, scaleCopy.y += activateIncrement);
            yield return new WaitForSeconds(activateIncrementTime);
        }
        InvokeRepeating("DecreaseLightRadius",0f, 0.1f);
    }
    
    public void DecreaseLightRadius()
    {
        var scaleCopy = transform.localScale;
        myLight.transform.localScale = new Vector2(scaleCopy.x -= dimRate, scaleCopy.y -= dimRate);
        if (myLight.transform.localScale.x <= 0)
        {
            animator.SetTrigger("LanternOff");
            myLight.intensity = 0;
            myLight.transform.localScale = new Vector2(0, 0);
            currentlyLit = false;
            //currentlyLit = false; we dont want it to be able to be lit a 2nd time
        }
    }
}
