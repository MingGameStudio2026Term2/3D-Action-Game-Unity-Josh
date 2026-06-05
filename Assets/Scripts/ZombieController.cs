using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    public Transform targetCube;

    [Header("Health System")]
    public float maxHealth = 100f;
    public float currentHealth;
    public GameObject healthBarPrefab; // Assign a world-space canvas prefab with HealthBar script
    private GameObject healthBarInstance;
    private HealthBar healthBar;

    [Header("Drop Settings")]
    public GameObject coinPrefab; // Assign your coin prefab here

    [Header("Attack Settings")]
    public float attackDistance = 1.5f; // How far does the zombie stop and start attack
    public float attackInterval = 2.0f; // Interval between attacks in seconds
    private float lastAttackTime = -Mathf.Infinity;
    private bool isDead = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;

        // Instantiate health bar UI
        if (healthBarPrefab != null)
        {
            healthBarInstance = Instantiate(healthBarPrefab, transform.position + Vector3.up * 2f, Quaternion.identity, null);
            healthBar = healthBarInstance.GetComponentInChildren<HealthBar>();
            if (healthBar != null)
            {
                healthBar.SetHealth(currentHealth, maxHealth);
            }
        }
    }

    void Update()
    {
        if (isDead) return;

        if (targetCube != null)
        {
            float distance = Vector3.Distance(transform.position, targetCube.position);
            if (distance <= attackDistance)
            {
                // Stop moving
                agent.isStopped = true;
                animator.SetFloat("Speed", 0f);

                // Attack if enough time has passed
                if (Time.time - lastAttackTime >= attackInterval)
                {
                    animator.SetTrigger("Attack");
                    lastAttackTime = Time.time;
                }
            }
            else
            {
                // Move toward target
                agent.isStopped = false;
                agent.SetDestination(targetCube.position);
                if (animator != null && agent != null)
                {
                    float speed = agent.velocity.magnitude;
                    animator.SetFloat("Speed", speed);
                }
            }
        }

        // Update health bar position and value
        if (healthBarInstance != null)
        {
            healthBarInstance.transform.position = transform.position + Vector3.up * 2f;
            healthBarInstance.transform.LookAt(Camera.main.transform);
            if (healthBar != null)
            {
                healthBar.SetHealth(currentHealth, maxHealth);
            }
        }
    }

    public void TakeDamage(float amount)
    {
        Debug.Log($"Zombie {gameObject.name} takes {amount} damage. Current health before: {currentHealth}");
        currentHealth -= amount;
        if (currentHealth <= 0f)
        {
            Debug.Log($"Zombie {gameObject.name} died.");
            Die();
        }
        else
        {
            Debug.Log($"Zombie {gameObject.name} health after damage: {currentHealth}");
        }
    }

    void Die()
    {
        isDead = true;

        // Drop coin
        if (coinPrefab != null)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }

        // Destroy health bar
        if (healthBarInstance != null)
        {
            Destroy(healthBarInstance);
        }
        if (animator != null)
        {
            animator.SetTrigger("Died");
        }
        // Disable movement and collisions
        if (agent != null)
        {
            agent.isStopped = true;
            agent.enabled = false;
        }
        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;
        // Destroy after animation (adjust delay as needed)
        Destroy(gameObject, 2.0f);
    }
}
