using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This script takes the agent component and transforms it to our desired position, AKA left and right.
/// </summary>
public class LeftRightNPC : MonoBehaviour
{
    private NavMeshAgent agent;
    public float moveDistance = 10f;
    public float moveSpeed = 2f;
    private Vector3 leftPoint;
    private Vector3 rightPoint;
    private Vector3 currentTarget;
    private bool movingRight = true;

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

        leftPoint = transform.position - transform.right * moveDistance;
        rightPoint = transform.position + transform.right * moveDistance;
        currentTarget = rightPoint;

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
                if (movingRight)
                {
                    currentTarget = leftPoint;
                    movingRight = false;
                }
                else
                {
                    currentTarget = rightPoint;
                    movingRight = true;
                }
                agent.SetDestination(currentTarget);
            }
        }
    }
}