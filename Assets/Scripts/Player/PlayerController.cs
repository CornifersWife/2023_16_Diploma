using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IWalkable {
    [SerializeField] private InputAction mouseClickAction;
    private Camera mainCamera;
    private NavMeshAgent navMeshAgent;
    
    private Vector3 targetPosition;
    private int groundLayer;

    private void Awake() {
        mainCamera = Camera.main;
        navMeshAgent = GetComponent<NavMeshAgent>();
        groundLayer = LayerMask.NameToLayer("Ground");
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
        SetTargetPoint();
        Walk(targetPosition);
    }
    
    public void SetTargetPoint() {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider && hit.collider.gameObject.layer.CompareTo(groundLayer) == 0) {
            targetPosition = hit.point;
        }
    }

    public void Walk(Vector3 target) {
        navMeshAgent.SetDestination(target);
    }
    
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(targetPosition, 0.3f);
    }

}
