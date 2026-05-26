using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Header("Effects")]
    public GameObject hitParticlePrefab; // Assign your particle effect prefab in Inspector

    void OnTriggerEnter(Collider other)
    {
        // Check if hit a zombie
        if (other.GetComponent<ZombieController>() != null)
        {
            if (hitParticlePrefab != null)
            {
                Instantiate(hitParticlePrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject); // Destroy bullet on hit
        }
    }
}
