using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.UI.Image;

public class ProxyVisionGetData : MonoBehaviour
{
    public int rayCount = 36;
    public float radius = 10f;

    public UnityEvent<Transform> HitPlayer;

    void Update()
    {
        for (int i = 0; i < rayCount; i++)
        {
            float angle = i * (360f / rayCount);
            Vector3 direction = Quaternion.Euler(0, angle, 0) * transform.forward;

            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, radius))
            {
                if (hit.collider.tag == "Player")
                {
                    Debug.DrawLine(transform.position, hit.point, Color.red);
                    Debug.Log("Hit: " + hit.collider.name);
                    HitPlayer.Invoke(hit.transform);
                }
            }
            else
            {
                Debug.DrawRay(transform.position, direction * radius, Color.yellow);
            }
        }
    }
}
