using UnityEngine;
using UnityEngine.Events;

public class OrbitActionManager : MonoBehaviour
{
    public UnityEvent<Transform> moveEvent;

    public void AttackPlayer(Transform transform)
    {
        moveEvent.Invoke(transform);
    }
}
