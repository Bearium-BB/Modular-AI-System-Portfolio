using UnityEngine;
using UnityEngine.Events;

public class ActionManager : MonoBehaviour
{
    public UnityEvent<Transform> moveEvent;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void AttackPlayer(Transform transform) 
    {
        moveEvent.Invoke(transform);
    }
}
