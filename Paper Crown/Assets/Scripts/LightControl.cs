using Unity.VisualScripting;
using UnityEngine;

public class LightControl : MonoBehaviour
{

    public float shrinkAmountOnDamage = 1f;  // Amount to shrink on damage
    public Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale; // Store original scale on start
    }

    public void TakeDamage()
    {
        Vector3 currentScale = transform.localScale;

        float newScale = Mathf.Max(currentScale.x - 2f, 0f);

        transform.localScale = new Vector3(newScale, newScale, currentScale.z);
    }

    public void ResetLight()
    {
        transform.localScale = originalScale;
    }
}