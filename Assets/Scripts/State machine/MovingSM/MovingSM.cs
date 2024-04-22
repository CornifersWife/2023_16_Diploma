using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MovingSM : StateMachine, IPointerDownHandler {
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
    private bool isClickTarget;

    private void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        
        idleState = new IdleState(this);
        walkingState = new WalkingState(this);
        waitingState = new WaitingState(this);
        dialogueState = new DialogueState(this);
    }

    //TODO chcek it some other way, not every frame

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
    
    public bool IsDialogue() {
        UIManager uiManager = UIManager.Instance;
        bool isUIOpen = uiManager.GetCurrentUICount() > uiManager.GetUICountOnStart();
        //Debug.Log("Target: " + isClickTarget);
        //Debug.Log("UI: " + isUIOpen);
        return isClickTarget && isUIOpen;
    }

    public void OnPointerDown(PointerEventData eventData) {
        Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
        isClickTarget = eventData.pointerCurrentRaycast.gameObject == gameObject;
    }
}
