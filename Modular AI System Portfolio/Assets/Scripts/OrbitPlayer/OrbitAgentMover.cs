using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class OrbitAgentMover : MonoBehaviour
{
    Transform player;
    public float radius = 5f;
    public float orbitSpeed = 30f;
    public float moveSpeed = 2f;

    private float angle;
    private bool isOrbiting = false;

    private void Update()
    {
        if (player != null)
        {
            Vector3 toPlayer = transform.position - player.position;

            if (!isOrbiting)
            {
                Vector3 flatToStar = new Vector3(toPlayer.x, 0, toPlayer.z).normalized;
                Vector3 targetPos = player.position + flatToStar * radius;

                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, targetPos) < 0.05f)
                {
                    angle = Mathf.Atan2(flatToStar.x, flatToStar.z) * Mathf.Rad2Deg;
                    isOrbiting = true;
                }
            }
            else
            {
                angle += orbitSpeed * Time.deltaTime;
                float radians = angle * Mathf.Deg2Rad;

                Vector3 offset = new Vector3(Mathf.Sin(radians), 0, Mathf.Cos(radians)) * radius;
                transform.position = player.position + offset;
            }
        }

    }

    public void UpdatePosition(Transform pos)
    {
        player = pos;
    }
}
