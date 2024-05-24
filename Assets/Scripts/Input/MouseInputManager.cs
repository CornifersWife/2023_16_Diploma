using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class MouseInputManager : MonoBehaviour {
    [SerializeField] private InputAction mouseClickAction;
    [SerializeField] private ParticleSystem clickEffect;
    [SerializeField] private GameObject player;
    private Camera mainCamera;
    private NavMeshAgent navMeshAgent;
    
    private Vector3 targetPosition;
    private int groundLayer;
    private bool mouseClickEnabled = true;

    private void Awake() {
        mainCamera = Camera.main;
        navMeshAgent = player.GetComponent<NavMeshAgent>();
        groundLayer = LayerMask.NameToLayer("Ground");
        targetPosition = player.transform.position;
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
        if (!ManageGame.Instance.IsStarted || !mouseClickEnabled)
            return;
        SetTargetPoint();
    }
    
    private void SetTargetPoint() {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider && hit.collider.gameObject.layer.CompareTo(groundLayer) == 0) {
            targetPosition = hit.point;
            Instantiate(clickEffect, targetPosition += new Vector3(0, 0.1f, 0), clickEffect.transform.rotation);
            navMeshAgent.SetDestination(targetPosition);
        }
    }

    public void EnableMouseControls() {
        mouseClickEnabled = true;
    }
    
    public void DisableMouseControls() {
        if(ManageGame.Instance.IsStarted)
            navMeshAgent.ResetPath();
        mouseClickEnabled = false;
    }
}
