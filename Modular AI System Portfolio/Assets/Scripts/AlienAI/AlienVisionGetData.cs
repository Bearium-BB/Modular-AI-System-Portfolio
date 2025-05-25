using UnityEngine;
using UnityEngine.Events;

public class AlienVisionGetData : MonoBehaviour
{
    public float viewRadius = 10f;
    public float viewAngle = 90f;
    public int horizontalResolution = 20;
    public int verticalResolution = 10;
    public UnityEvent<AlienVisionModel> visionData = new UnityEvent<AlienVisionModel>();
    // This is allocating space for raycast hits for performance
    RaycastHit[] hits = new RaycastHit[10];
    public LayerMask visionMask;

    void Update()
    {
        Cast3DVisionCone();
    }

    void Cast3DVisionCone()
    {
        Vector3 origin = transform.position;
        bool playerSeen = false;

        for (int y = 0; y <= verticalResolution && !playerSeen; y++)
        {
            //Stack overflow with some modifications
            float verticalAngle = Mathf.Lerp(-viewAngle / 2, viewAngle / 2, (float)y / verticalResolution);
            //Stack overflow with some modifications


            for (int x = 0; x <= horizontalResolution && !playerSeen; x++)
            {
                //Stack overflow with some modifications
                float horizontalAngle = Mathf.Lerp(-viewAngle / 2, viewAngle / 2, (float)x / horizontalResolution);
                //Stack overflow with some modifications

                Vector3 dir = DirFromAngles(horizontalAngle, verticalAngle);

                // This gets the closest hit array and see how many were hit as well
                int hitCount = Physics.RaycastNonAlloc(origin, dir, hits, viewRadius, visionMask);

                // Prevents AI from seeing through walls
                // Find the closest hit
                if (hitCount > 0)
                {
                    RaycastHit closestHit = hits[0];
                    for (int i = 1; i < hitCount; i++)
                    {
                        if (hits[i].distance < closestHit.distance)
                        {
                            closestHit = hits[i];
                        }
                    }

                    if (closestHit.collider.CompareTag("Player"))
                    {
                        Debug.DrawRay(origin, dir * closestHit.distance, Color.red);
                        visionData.Invoke(new AlienVisionModel(closestHit.collider.transform, true));
                        playerSeen = true;
                    }
                    else
                    {
                        Debug.DrawRay(origin, dir * closestHit.distance, Color.cyan);
                    }
                }

            }

        }

        if (!playerSeen)
        {
            visionData.Invoke(new AlienVisionModel(null, false));
        }
    }

    //ChatGPT couldn't figure out how to get the rotation done properly—really bad at rotation math. I hope we get to learn a lot about rotation math in the course
    Vector3 DirFromAngles(float horizontalAngle, float verticalAngle)
    {
        Quaternion rotation = Quaternion.Euler(transform.eulerAngles.x + verticalAngle, transform.eulerAngles.y + horizontalAngle, 0);
        return rotation * Vector3.forward;
    }
    //ChatGPT couldn't figure out how to get the rotation done properly—really bad at rotation math. I hope we get to learn a lot about rotation math in the course

}

public class AlienVisionModel
{
    public Transform pos;
    public bool isEnvision;

    public AlienVisionModel(Transform Pos, bool isEnvision) 
    { 
        this.pos = Pos;
        this.isEnvision = isEnvision;
    }
}