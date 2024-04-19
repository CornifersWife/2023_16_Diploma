using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class MovingSM : StateMachine {
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
    
    public bool IsDialogue() {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        bool isTarget = false;

        //TODO check this some other way, this solution checks every frame and we don't want that
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider) {
            isTarget = hit.collider.gameObject == gameObject;
        }

        UIManager uiManager = UIManager.Instance;
        bool isUIOpen = uiManager.GetCurrentUICount() > uiManager.GetUICountOnStart();
        return isTarget && isUIOpen;
    }
}
