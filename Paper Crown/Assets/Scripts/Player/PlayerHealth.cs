using UnityEngine;

// Added health feature, it sets the health to 10 first thing, then updates the health and health
// counter whenever an object of tag "Damage" is touched
// RFA 14/09/2025

// Added smart health, it calculates the health the player should have based off of the light
// radius of the sword
// RFA 22/10/2025

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public TMPro.TextMeshProUGUI healthCounterText;
    public Knockback knockback;
    public LightControl playerLight;
    public int originalHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //int ShrinkAmount = (int)playerLight.shrinkAmountOnDamage;
        //int Radius = (int)playerLight.transform.localScale.x;
        //health = Radius / ShrinkAmount;
        //originalHealth = health;
        healthCounterText.text = health.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
}
