using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class MovingSM : StateMachine, IPointerClickHandler {
    [HideInInspector]
    public IdleState idleState;
    [HideInInspector]
    public WalkingState walkingState;
    [HideInInspector]
    public WaitingState waitingState;
    [HideInInspector]
    public DialogueState dialogueState;
    
    [SerializeField] private float waitTimeSeconds;
    [SerializeField] private Transform[] waypoints;
    
    private NavMeshAgent navMeshAgent;
    private int currentWaypointIndex;
    private bool waiting = false;
    public bool IsDialogue { get; set; } = false;

    private void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        
        idleState = new IdleState(this);
        walkingState = new WalkingState(this);
        waitingState = new WaitingState(this);
        dialogueState = new DialogueState(this);
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
    
    //TODO rework this
    public void OnPointerClick(PointerEventData eventData) {
        IsDialogue = true;
    }
}
