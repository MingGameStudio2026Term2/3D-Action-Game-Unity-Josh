using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    public Transform targetCube;

    [Header("Attack Settings")]
    public float attackDistance = 1.5f; // How far does the zombie stop and start attack
    public float attackInterval = 2.0f; // Interval between attacks in seconds
    private float lastAttackTime = -Mathf.Infinity;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
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
    }
}
