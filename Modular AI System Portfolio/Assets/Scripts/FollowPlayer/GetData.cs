using UnityEngine;
using UnityEngine.Events;

public class GetData : MonoBehaviour
{
    public float viewRadius = 10f;
    public float viewAngle = 90f;
    public int horizontalResolution = 20;
    public int verticalResolution = 10;
    public UnityEvent<Transform> HitPlayer;
    void Update()
    {
        Cast3DVisionCone();
    }

    void Cast3DVisionCone()
    {
        for (int y = 0; y <= verticalResolution; y++)
        {
            float verticalAngle = Mathf.Lerp(-viewAngle / 2, viewAngle / 2, (float)y / verticalResolution);

            for (int x = 0; x <= horizontalResolution; x++)
            {
                float horizontalAngle = Mathf.Lerp(-viewAngle / 2, viewAngle / 2, (float)x / horizontalResolution);

                Vector3 dir = DirFromAngles(horizontalAngle, verticalAngle);
                Vector3 origin = transform.position;

                if (Physics.Raycast(origin, dir, out RaycastHit hit, viewRadius))
                {
                    if (hit.collider.tag == "Player")
                    {
                        Debug.DrawRay(origin, dir * hit.distance, Color.red);
                        Debug.Log("Hit: " + hit.collider.name);
                        HitPlayer.Invoke(hit.collider.transform);
                    }
                }
                else
                {
                    Debug.DrawRay(origin, dir * viewRadius, Color.yellow);
                }
            }
        }
    }

    Vector3 DirFromAngles(float horizontalAngle, float verticalAngle)
    {
        Quaternion rotation = Quaternion.Euler(transform.eulerAngles.x + verticalAngle, transform.eulerAngles.y + horizontalAngle, 0);
        return rotation * Vector3.forward;
    }
}
