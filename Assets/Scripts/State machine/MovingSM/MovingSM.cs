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
    [SerializeField] private InputAction mouseClickAction;
    
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

    private void OnEnable() {
        mouseClickAction.Enable();
        mouseClickAction.performed += CheckTarget;
    }

    private void OnDisable() {
        mouseClickAction.Disable();
        mouseClickAction.performed -= CheckTarget;
    }

    //TODO chcek it some other way, not every frame
    private void CheckTarget(InputAction.CallbackContext context) {
        Vector3 mousePos = Mouse.current.position.ReadValue();

        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider) {
            isClickTarget = hit.collider.gameObject == gameObject;
        }
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
        UIManager uiManager = UIManager.Instance;
        bool isUIOpen = uiManager.GetCurrentUICount() > uiManager.GetUICountOnStart();
        return isClickTarget && isUIOpen;
    }
}
