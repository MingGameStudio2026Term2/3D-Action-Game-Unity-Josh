using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [Header("References")]
    public Transform greenBar;

    private Vector3 initialScale;

    void Awake()
    {
        if (greenBar == null)
        {
            greenBar = transform;
        }

        initialScale = greenBar.localScale;
    }

    public void SetHealth(float currentHealth, float maxHealth)
    {
        float ratio = maxHealth > 0f ? Mathf.Clamp01(currentHealth / maxHealth) : 0f;
        Vector3 scale = initialScale;
        scale.x = initialScale.x * ratio;
        greenBar.localScale = scale;
    }
}
