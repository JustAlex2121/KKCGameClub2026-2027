using System;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class NavAgentMover : MonoBehaviour
{
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public bool HasArrived => agent.isOnNavMesh && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance + 0.1f;

    internal void MoveToPos(Vector3 vector3)
    {
        agent.SetDestination(vector3);
        agent.isStopped = false;
    }

    internal void MoveTo(Transform target)
    {
        MoveToPos(target.position);
    }
    
    internal void Stop()
    {
        agent.isStopped = true;
        agent.ResetPath();
    }
}
