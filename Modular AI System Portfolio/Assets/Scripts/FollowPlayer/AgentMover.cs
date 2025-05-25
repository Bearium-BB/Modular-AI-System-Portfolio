using UnityEngine;
using UnityEngine.AI;

public class AgentMover : MonoBehaviour
{
    public NavMeshAgent agent;
    Transform playerTransform;

    public void Move(Transform pos)
    {
        if (pos != null)
        {
            playerTransform = pos;
        }

        if (playerTransform != null)
        {
            agent.destination = playerTransform.position;
            Vector3 direction = playerTransform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
        }

    }
}
