using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DragAndDrop : MonoBehaviour {
    public Vector3 mousePosition;
    public bool validTarget;
    public Vector3 startingPosition;

   
    
    [SerializeField] float snapDistance = 2.0f;


    //private GameObject currentSnapTarget;
    private Vector3 GetMousePos() {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseUp() {
        FindTargetToSnapTo();
    }

    private void OnMouseDown() {
        mousePosition = Input.mousePosition - GetMousePos();
        startingPosition = transform.position;
    }

    private void OnMouseDrag() {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
    }

    private void FindTargetToSnapTo() {
        var snapTargets = FindObjectsOfType<CardSpot>().Where(cardSpot => cardSpot.IsValid()).ToArray();

        var closestTarget = snapTargets
            .OrderBy(t => (t.transform.position - transform.position).sqrMagnitude)
            .FirstOrDefault();

        if (closestTarget is null) {
            SnapBack();
            return;
        }

        if ((closestTarget.transform.position - transform.position).sqrMagnitude <= snapDistance * snapDistance) {
            transform.position = closestTarget.transform.position;
            PlayCardAt(closestTarget);
        }
        else {
            SnapBack();
        }
    }

    private void SnapBack() {
        transform.position = startingPosition;
    }

    private void PlayCardAt(CardSpot target) {
        target.CardDisplay = this.gameObject;
    }
}