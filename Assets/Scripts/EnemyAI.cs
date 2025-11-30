using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This script takes the agent component and transforms it to our desired position.
/// We get to our desired position by referencing the player's transform.
/// </summary>
public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform playerTarget;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.speed = 3.5f;
        agent.angularSpeed = 120f;
        agent.acceleration = 8f;
        agent.stoppingDistance = 1.5f;
        agent.autoBraking = true;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }

    void Update()
    {
        if (playerTarget != null && agent.isActiveAndEnabled && agent.isOnNavMesh)
        {
            agent.SetDestination(playerTarget.position);
        }
    }

    void OnDrawGizmos()
    {
        if (agent != null && agent.hasPath)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, agent.destination);
        }
    }
}