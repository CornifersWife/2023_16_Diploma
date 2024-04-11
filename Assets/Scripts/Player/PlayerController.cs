using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IWalkable {
    [SerializeField] private InputAction mouseClickAction;
    private Camera mainCamera;
    private NavMeshAgent navMeshAgent;
    
    private Vector3 targetPosition;
    private int groundLayer;
    private GameObject UIGameObject;
    private int activeUIOnStart;

    private void Awake() {
        mainCamera = Camera.main;
        navMeshAgent = GetComponent<NavMeshAgent>();
        groundLayer = LayerMask.NameToLayer("Ground");
        UIGameObject = GameObject.Find("UI");
        activeUIOnStart = GetActiveUIOnStart();
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
        Vector3 mousePos = Mouse.current.position.ReadValue();
        PointerEventData eventData = new(EventSystem.current)
        {
            position = mousePos
        };
        List<RaycastResult> results = new();
        EventSystem.current.RaycastAll(eventData, results);
        Debug.Log(results.Count);
        if (results.Count > activeUIOnStart)
            return;

        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider && hit.collider.gameObject.layer.CompareTo(groundLayer) == 0) {
            targetPosition = hit.point;
        }
    }

    public void Walk(Vector3 target) {
        navMeshAgent.SetDestination(target);
    }

    private int GetActiveUIOnStart() {
        int count = 0;
        foreach (Transform uiChild in UIGameObject.transform) {
            if (uiChild.gameObject.activeSelf)
                count++;
        }
        return count;
    }
    
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(targetPosition, 0.3f);
    }

}
