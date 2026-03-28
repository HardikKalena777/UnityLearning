using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public State currentState;
    public List<Transform> waypoints = new List<Transform>();

    public float detectionRange = 10f;
    public float stoppingDistance = 3f;

    public float maxMoveSpeed = 5f;
    public float rotationSpeed = 3f;

    public string playerTag;

    private GameObject player;
    private float currentSpeed;
    private float distanceToPlayer;

    private int currentWaypointIndex = 0;

    private void Awake()
    {
        if (player == null) 
            player = GameObject.FindGameObjectWithTag(playerTag);

        currentSpeed = maxMoveSpeed;
        SetState(State.Roaming);
    }

    private void Update()
    {
        EvaluateState();
    }

    void EvaluateState()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer > detectionRange)
        {
            SetState(State.Roaming);
        }

        if (distanceToPlayer < detectionRange &&  distanceToPlayer > stoppingDistance)
        {
            SetState(State.Chasing);
        }

        if (distanceToPlayer < stoppingDistance)
        {
            SetState(State.None);
        }

    }

    void RoamAround()
    {
        if (waypoints == null || waypoints.Count <= 0)
        {
            Debug.LogError("Empty Waypoints please Assign Waypoints");
            return;
        }
        if (waypoints[currentWaypointIndex] == null)
        {
            Debug.LogError(waypoints[currentWaypointIndex] + "Is Empty Please assign Waypoint");
            return;
        }

        currentSpeed = maxMoveSpeed;

        Transform target = waypoints[currentWaypointIndex].transform;
        Vector3 targetPos = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, currentSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) <= 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
        }

        Vector3 direction = target.transform.position - transform.position;
        direction.y = 0f;
        if (direction.sqrMagnitude > 0.0001)
        {
            Quaternion targetRotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction.normalized), rotationSpeed * Time.deltaTime);
            transform.rotation = targetRotation;
        }
    }

    void ChasePlayer()
    {
        if (player == null)
        {
            Debug.LogError("Player is Null Please Assign Player");
            return;
        }

        currentSpeed = maxMoveSpeed;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, currentSpeed * Time.deltaTime);

        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0f;
        if (direction.sqrMagnitude > 0.0001)
        {
            Quaternion targetRotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction.normalized), rotationSpeed * Time.deltaTime);
            transform.rotation = targetRotation;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere (transform.position,stoppingDistance);
    }

    public void SetState(State state)
    {
        if (currentState != state)
        {
            currentState = state;
        }

        switch (state)
        {
            case State.None:
                currentSpeed = 0f;
                break;

            case State.Chasing:
                ChasePlayer();
                break;

            case State.Roaming:
                RoamAround();
                break;
        }
    }
}

public enum State
{
    Roaming,
    Chasing,
    None
}
