using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public class GivePlayerPositionGetData : MonoBehaviour
{
    public Transform playerPosition;
    public Vector3 oldPlayerPosition;
    public float radius = 10;
    public UnityEvent<Vector3> searchPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        oldPlayerPosition = playerPosition.position;
        searchPosition.Invoke(playerPosition.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GetPlayerDistance(playerPosition.position) >= radius)
        {
            searchPosition.Invoke(playerPosition.position);
            oldPlayerPosition = playerPosition.position;
        }
    }

    public float GetPlayerDistance(Vector3 pos)
    {
        float dist = Vector3.Distance(pos, oldPlayerPosition);
        return dist;
    }
}
