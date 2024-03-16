using System;
using System.Linq;
using TurnSystem;
using UnityEngine;

public class DragAndDrop : MonoBehaviour {
    public Vector3 mousePosition;
    public Vector3 startingPosition;
    private TurnManager turnManager;
    [SerializeField] float snapDistance = 2.0f;
    
    private void Awake() {
        turnManager = TurnManager.Instance;
    }

    private Vector3 GetMousePos() {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseUp() {
        if (!enabled) return; 
        FindTargetToSnapTo();
    }

    private void OnMouseDown() {
        if (!enabled) return; 

        mousePosition = Input.mousePosition - GetMousePos();
        startingPosition = transform.position;
    }

    private void OnMouseDrag() {
        if (!enabled) return; 
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
    }

    private void FindTargetToSnapTo() {
        if (!turnManager.isPlayerTurn) {
            SnapBack();
            return;
        }
        var targets = FindObjectsOfType<CardSpot>().Where(cardSpot => cardSpot.IsValid()).ToArray();

        var closestTarget = targets
            .OrderBy(t => (t.transform.position - transform.position).sqrMagnitude)
            .FirstOrDefault();

        if (closestTarget is null) {
            SnapBack();
            return;
        }

        if ((closestTarget.transform.position - transform.position).sqrMagnitude <= snapDistance * snapDistance) {
            PlayCardAt(closestTarget);
        }
        else {
            SnapBack();
        }
    }

    public void SnapBack() {
        transform.position = startingPosition;
    }

    private void PlayCardAt(CardSpot target) {
        target.CardDisplay = GetComponent<CardDisplay>();
    }
}