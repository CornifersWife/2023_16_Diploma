using UnityEngine;
using UnityEngine.AI;

public class MovingSM : StateMachine {
    [HideInInspector]
    public IdleState idleState;
    [HideInInspector]
    public WalkingState walkingState;
    [HideInInspector]
    public WaitingState waitingState;
    
    [SerializeField] private float waitTimeSeconds;
    [SerializeField] private Transform[] waypoints;
    private NavMeshAgent navMeshAgent;
    private int currentWaypointIndex;
    private bool waiting = false;

    private void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        
        idleState = new IdleState(this);
        walkingState = new WalkingState(this);
        waitingState = new WaitingState(this);
    }
    
    protected override BaseState GetInitialState() {
        return idleState;
    }

    public int GetCurrentWaypointIndex() {
        return currentWaypointIndex;
    }

    public void SetCurrentWaypointIndex(int value) {
        currentWaypointIndex = value;
    }

    public NavMeshAgent GetNavMeshAgent() {
        return navMeshAgent;
    }

    public float GetWaitTime() {
        return waitTimeSeconds;
    }

    public Transform[] GetWaypoints() {
        return waypoints;
    }

    public bool IsWaiting() {
        return waiting;
    }

    public void SetWaiting(bool value) {
        waiting = value;
    }
}