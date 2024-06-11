using System.Linq;
using Card;
using TurnSystem;
using UnityEngine;

public class DragAndDrop : MonoBehaviour {
    private Vector3 mousePosition;
    private Vector3 startingPosition;
    private TurnManagerOld turnManagerOld;
    [SerializeField] float snapDistance = 2.0f;
    
    private CardSpotOld potentialSnapTarget;

    private void Awake() {
        turnManagerOld = TurnManagerOld.Instance;
    }
    
    

    private void OnMouseDrag() {
        if (!enabled) return;
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
        if(turnManagerOld.isPlayerTurn)
            UpdatePotentialSnapTarget(); 
    }
    private void UpdatePotentialSnapTarget() {
        if (!turnManagerOld.isPlayerTurn) return;

        var targets = FindObjectsOfType<CardSpotOld>().Where(cardSpot => cardSpot.IsValid()).ToArray();
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
        if (!turnManagerOld.isPlayerTurn) {
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
    

    //TODO BUG when holding a cardOld and drawing new ones, after snapping back the cardOld will return to its original position, not the correct position that it should be at after after drawing a new cardOld
    public void SnapBack() {
        transform.position = startingPosition;
    }

    private void PlayCardAt(CardSpotOld target) {
        target.CardOld = GetComponent<CardOld>();
    }
}