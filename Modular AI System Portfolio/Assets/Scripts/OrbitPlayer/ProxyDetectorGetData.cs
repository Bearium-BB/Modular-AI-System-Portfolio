using UnityEngine;
using UnityEngine.Events;

public class ProxyDetectorGetData : MonoBehaviour
{
    public float radius = 10f;

    public UnityEvent<Transform> HitPlayer;

    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.tag == "Player")
            {
                HitPlayer.Invoke(hitCollider.transform);
            }
        }
    }
}
