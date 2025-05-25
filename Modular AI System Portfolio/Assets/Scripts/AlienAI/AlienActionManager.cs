using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using static Unity.Burst.Intrinsics.X86;

public class AlienActionManager : MonoBehaviour
{
    public State state = State.Searching;
    public UnityEvent<Vector3> searchForPlayer;
    public UnityEvent<Transform> chasePlayer;
    //How long it takes for the AI to go back to searching
    public float timeToSearch = 5f;

    private Vector3 searchPosition;
    //Times how long it hasn't seen a player
    private float loseSightTimer = 0f;

    AlienVisionModel alienVisionModel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //kind of just starts off the logic States
        SearchForPlayer(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(state);
        if (state == State.Chasing)
        {
            ChasePlayer();
            if (alienVisionModel.isEnvision)
            {
                loseSightTimer = 0f;
            }
            else
            {
                loseSightTimer += Time.deltaTime;

                if (loseSightTimer >= timeToSearch)
                {
                    state = State.Searching;
                }
            }
        }
    }
    //Changes the state and update the visual model
    public void CanSeePlayer(AlienVisionModel model)
    {
        alienVisionModel = model;
        if (model.isEnvision == true)
        {
            state = State.Chasing;
        }
        ChasePlayer(alienVisionModel.pos);
    }

    public void ChasePlayer(Transform transform)
    {
        chasePlayer.Invoke(transform);
    }

    public void ChasePlayer()
    {
        chasePlayer.Invoke(null);
    }

    public void UpdateSearchPosition(Vector3 pos)
    {
        searchPosition = pos;
    }

    public void RepeatSearch()
    {
        if (state == State.Searching)
        {
            SearchForPlayer();
        }
    }

    public void SearchForPlayer(Vector3 pos)
    {
        if (state == State.Searching)
        {
            searchForPlayer.Invoke(pos);
        }
    }

    public void SearchForPlayer()
    {
        if (state == State.Searching)
        {
            searchForPlayer.Invoke(searchPosition);
        }
    }

    public enum State
    {
        Searching,
        Chasing,
    }
}
