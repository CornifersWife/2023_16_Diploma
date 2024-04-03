using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour {
    [SerializeField] private float waitTimeSeconds;
    [SerializeField] private Transform[] waypoints;

    private NavMeshAgent navMeshAgent;
    private int currentWaypointIndex;
    private Vector3 currentWaypoint;
    private float waitCounter;
    private bool waiting = false;

    private void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        if (waiting)
            Wait();
        currentWaypoint = waypoints[currentWaypointIndex].position;
        currentWaypoint.y = transform.position.y;
        if (Vector3.Distance(transform.position, currentWaypoint) < 0.01f) {
            ResetTimer();
            SetWaypoint();
        }
        else {
            Move();
        }
    }

    private void Wait() {
        waitCounter += Time.deltaTime;
        if (waitCounter < waitTimeSeconds)
            return;
        waiting = false;
    }

    private void SetWaypoint() {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }

    private void ResetTimer() {
        waitCounter = 0f;
        waiting = true;
    }

    private void Move() {
        navMeshAgent.SetDestination(currentWaypoint);
    }
    
    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(currentWaypoint, 0.3f);
    }
}
