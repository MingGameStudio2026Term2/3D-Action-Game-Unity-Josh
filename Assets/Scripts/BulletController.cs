using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Header("Effects")]
    public GameObject hitParticlePrefab; // Assign your particle effect prefab in Inspector

    [Header("Bullet Settings")]
    public float damage = 25f;

    void OnTriggerEnter(Collider other)
    {
        // Check if hit a zombie
        ZombieController zombie = other.GetComponent<ZombieController>();
        if (zombie != null)
        {
            zombie.TakeDamage(damage);
            if (hitParticlePrefab != null)
            {
                Instantiate(hitParticlePrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject); // Destroy bullet on hit
        }
    }
}
