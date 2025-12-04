using System;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class LightControl : MonoBehaviour
{

    // Original code(delete maybe)
    // public Vector3 originalScale;

    public float shrinkAmountOnDamage = 1f;  // Amount to shrink on damage
    private float originalRange;
    private Light swordLight;

    void Start()
    {
        // Original code(delete maybe)
        // originalScale = transform.localScale; // Store original scale on start
        swordLight = GetComponent<Light>();
        originalRange = swordLight.range; // Store original range on start
    }

    public void TakeDamage()
    {
        Debug.Log("Ow!");
        swordLight.range -= shrinkAmountOnDamage;

        // Original code (delete maybe)
        //Vector3 currentScale = transform.localScale;

        //float newScale = Mathf.Max(currentScale.x - 2f, 0f);

        //transform.localScale = new Vector3(newScale, newScale, currentScale.z);
    }

    public void ResetLight()
    {
        // Original code (delete maybe)
        // transform.localScale = originalScale;
        swordLight.range = originalRange;
    }
}