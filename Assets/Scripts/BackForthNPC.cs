using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This script takes the agent component and transforms it to our desired position, AKA back and forth.
/// </summary>
public class BackForthNPC : MonoBehaviour
{
    private NavMeshAgent agent;
    public float moveDistance = 10f;
    public float moveSpeed = 2f;
    private Vector3 startPosition;
    private Vector3 forwardPoint;
    private Vector3 backPoint;
    private Vector3 currentTarget;
    private bool movingForward = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        if (!agent.isOnNavMesh)
        {
            PlaceOnNavMesh();
        }

        startPosition = transform.position;
        forwardPoint = startPosition + transform.forward * moveDistance;
        backPoint = startPosition - transform.forward * moveDistance;
        currentTarget = forwardPoint;

        agent.speed = moveSpeed;
        agent.SetDestination(currentTarget);
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
        if (agent.isActiveAndEnabled && agent.isOnNavMesh)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                if (movingForward)
                {
                    currentTarget = backPoint;
                    movingForward = false;
                }
                else
                {
                    currentTarget = forwardPoint;
                    movingForward = true;
                }
                agent.SetDestination(currentTarget);
            }
        }
    }
}