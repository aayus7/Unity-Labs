using UnityEngine;
using UnityEngine.AI;

public class NPCMove : MonoBehaviour
{
    public Transform[] waypoints;
    private int index = 0;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetDestination();
    }

    void Update()
    {
        // If NPC is close to the waypoint, go to the next one
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            index = (index + 1) % waypoints.Length;
            SetDestination();
        }
    }

    void SetDestination()
    {
        if (waypoints.Length > 0)
            agent.destination = waypoints[index].position;
    }
}