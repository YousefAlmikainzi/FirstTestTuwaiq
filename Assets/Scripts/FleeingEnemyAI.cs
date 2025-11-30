using UnityEngine;
using UnityEngine.AI;

public class FleeingEnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform playerTarget;
    public float fleeDistance = 10f;
    public float safeDistance = 15f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.speed = 5f;
        agent.angularSpeed = 120f;
        agent.acceleration = 10f;
        agent.stoppingDistance = 0f;
        agent.autoBraking = false;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        if (!agent.isOnNavMesh)
        {
            PlaceOnNavMesh();
        }
    }

    void PlaceOnNavMesh()
    {
        if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 10.0f, NavMesh.AllAreas))
        {
            transform.position = hit.position;
        }
    }

    void Update()
    {
        if (playerTarget != null && agent.isActiveAndEnabled && agent.isOnNavMesh)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTarget.position);

            if (distanceToPlayer < fleeDistance)
            {
                FleeFromPlayer();
            }
            else if (distanceToPlayer >= safeDistance && !agent.hasPath)
            {
                agent.ResetPath();
            }
        }
    }

    void FleeFromPlayer()
    {
        Vector3 fleeDirection = transform.position - playerTarget.position;
        fleeDirection.Normalize();

        Vector3 fleePosition = transform.position + fleeDirection * safeDistance;

        if (NavMesh.SamplePosition(fleePosition, out NavMeshHit hit, safeDistance, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
        else
        {
            Vector3 randomDirection = Random.insideUnitSphere * safeDistance;
            randomDirection.y = 0;
            Vector3 alternativeFleePosition = transform.position + randomDirection;

            if (NavMesh.SamplePosition(alternativeFleePosition, out NavMeshHit hit2, safeDistance, NavMesh.AllAreas))
            {
                agent.SetDestination(hit2.position);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, fleeDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, safeDistance);

        if (agent != null && agent.hasPath)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, agent.destination);
        }
    }
}