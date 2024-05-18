using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class MouseInputManager : MonoBehaviour {
    [SerializeField] private InputAction mouseClickAction;
    private Camera mainCamera;
    private NavMeshAgent navMeshAgent;
    
    private Vector3 targetPosition;
    private int groundLayer;
    private bool mouseClickEnabled = true;

    private void Awake() {
        mainCamera = Camera.main;
        navMeshAgent = GetComponent<NavMeshAgent>();
        groundLayer = LayerMask.NameToLayer("Ground");
        targetPosition = transform.position;
    }

    private void OnEnable() {
        mouseClickAction.Enable();
        mouseClickAction.performed += MovePlayer;
    }

    private void OnDisable() {
        mouseClickAction.performed -= MovePlayer;
        mouseClickAction.Disable();
    }
    
    private void MovePlayer(InputAction.CallbackContext context) {
        if (!mouseClickEnabled)
            return;
        SetTargetPoint();
    }
    
    private void SetTargetPoint() {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider && hit.collider.gameObject.layer.CompareTo(groundLayer) == 0) {
            targetPosition = hit.point;
            Walk(targetPosition);
        }
    }

    private void Walk(Vector3 target) {
        navMeshAgent.SetDestination(target);
    }

    public void EnableMouseControls() {
        mouseClickEnabled = true;
    }
    
    public void DisableMouseControls() {
        navMeshAgent.ResetPath();
        mouseClickEnabled = false;
    }
}
