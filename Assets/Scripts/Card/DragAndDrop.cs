using System.Linq;
using TurnSystem;
using UnityEngine;

public class DragAndDrop : MonoBehaviour {
    private Vector3 mousePosition;
    private Vector3 startingPosition;
    private TurnManager turnManager;
    [SerializeField] float snapDistance = 2.0f;
    
    private CardSpot potentialSnapTarget;

    private void Awake() {
        turnManager = TurnManager.Instance;
    }
    
    

    private void OnMouseDrag() {
        if (!enabled) return;
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
        if(turnManager.isPlayerTurn)
            UpdatePotentialSnapTarget(); 
    }
    private void UpdatePotentialSnapTarget() {
        if (!turnManager.isPlayerTurn) return;

        var targets = FindObjectsOfType<CardSpot>().Where(cardSpot => cardSpot.IsValid()).ToArray();
        var closestTarget = targets
            .OrderBy(t => (t.transform.position - transform.position).sqrMagnitude)
            .FirstOrDefault();

        if (closestTarget != null && (closestTarget.transform.position - transform.position).sqrMagnitude <= snapDistance * snapDistance) {
            if (potentialSnapTarget != closestTarget) {
                potentialSnapTarget?.ClearHighlight();
                potentialSnapTarget = closestTarget;
                potentialSnapTarget.Highlight();
            }
        } else {
            potentialSnapTarget?.ClearHighlight();
            potentialSnapTarget = null;
        }
    }
    private void FindTargetToSnapTo() {
        if (!turnManager.isPlayerTurn) {
            SnapBack();
            return;
        }
        if (potentialSnapTarget is null) {
            SnapBack();
        } else {
            PlayCardAt(potentialSnapTarget);
            potentialSnapTarget.ClearHighlight();
            potentialSnapTarget = null; 
        }
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
    

    //TODO BUG when holding a card and drawing new ones, after snapping back the card will return to its original position, not the correct position that it should be at after after drawing a new card
    public void SnapBack() {
        transform.position = startingPosition;
    }

    private void PlayCardAt(CardSpot target) {
        target.Card = GetComponent<Card>();
    }
}