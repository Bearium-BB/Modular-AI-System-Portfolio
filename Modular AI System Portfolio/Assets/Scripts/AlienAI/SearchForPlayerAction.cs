using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class SearchForPlayerAction : MonoBehaviour
{
    public NavMeshAgent agent;
    public UnityEvent repeatAction;
    public float radius = 10;

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                Debug.Log("Destination reached!");
                repeatAction.Invoke();
            }
        }
    }

    public void RandomPoint(Vector3 pos)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += pos;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
            Debug.Log("Found valid position: " + hit.position);

        }
        else
        {
            Debug.Log("No valid NavMesh position found near: " + pos);
        }
    }
}
