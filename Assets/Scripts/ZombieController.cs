using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform targetCube;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        
    }

    void Update()
    {
        // Optionally update destination if the cube moves
        if (targetCube != null)
        {
            agent.SetDestination(targetCube.position);
        }
    }
}
