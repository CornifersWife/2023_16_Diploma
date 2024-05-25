using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class MouseInputManager : MonoBehaviour {
    [SerializeField] private InputAction mouseClickAction;
    [SerializeField] private ParticleSystem clickEffect;
    private Camera mainCamera;
    private NavMeshAgent navMeshAgent;
    
    private Vector3 targetPosition;
    private int groundLayer;
    private bool mouseClickEnabled = true;
    [SerializeField] private Animator animator;
    private static readonly int IsMoving = Animator.StringToHash("isMoving");

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
            Instantiate(clickEffect, hit.point += new Vector3(0, 0.1f, 0), clickEffect.transform.rotation);
            Walk(targetPosition);
            StopAllCoroutines();
            StartCoroutine(Wait());
        }
    }

    private void Walk(Vector3 target) {
        animator.SetBool(IsMoving, true);
        navMeshAgent.SetDestination(target);
    }

    private IEnumerator Wait() {
        bool isDone = false;
        
        while (!isDone) {
            if (!navMeshAgent.pathPending) {
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance) {
                    if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f) {
                        isDone = true;
                    }
                }
            }
            yield return null;
        } 
        animator.SetBool(IsMoving, false);
    }

    public void EnableMouseControls() {
        mouseClickEnabled = true;
    }
    
    public void DisableMouseControls() {
        navMeshAgent.ResetPath();
        mouseClickEnabled = false;
    }
}
