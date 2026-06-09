using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Header("Effects")]
    public GameObject hitParticlePrefab; // Assign your particle effect prefab in Inspector

    [Header("Bullet Settings")]
    public float damage = 25f;
    public float moveSpeed = 20f;
    public float reachDistance = 0.25f;

    private Transform target;
    private Rigidbody rb;
    private bool hasHit = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (hasHit || target == null) return;

        Vector3 targetPos = target.position + Vector3.up * 1f;
        Vector3 dir = (targetPos - transform.position).normalized;

        if (rb != null)
        {
            rb.velocity = dir * moveSpeed;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        }

        transform.forward = dir;

        if (Vector3.Distance(transform.position, targetPos) <= reachDistance)
        {
            ZombieController zombie = target.GetComponent<ZombieController>();
            if (zombie != null)
            {
                HitZombie(zombie);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetTarget(Transform newTarget, float speed)
    {
        target = newTarget;
        moveSpeed = speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (hasHit) return;

        // Check if hit a zombie
        ZombieController zombie = other.GetComponent<ZombieController>();
        if (zombie != null)
        {
            HitZombie(zombie);
        }
    }

    void HitZombie(ZombieController zombie)
    {
        hasHit = true;
        zombie.TakeDamage(damage);
        if (hitParticlePrefab != null)
        {
            Instantiate(hitParticlePrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
