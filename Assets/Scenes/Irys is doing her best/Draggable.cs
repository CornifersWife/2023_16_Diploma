using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {
    private Vector3 offset;
    private Camera mainCamera;
    private Transform originalTransform;

    private void Start() {
        mainCamera = Camera.main;
    }

    public void OnPointerDown(PointerEventData eventData) {
    }

    public void OnDrag(PointerEventData eventData) {
        var startingPosition = transform.position;
        this.transform.position = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData) {
    }
    
    public void OnBeginDrag(PointerEventData eventData) {
        originalTransform = transform;
    }

    public void OnEndDrag(PointerEventData eventData) {
        throw new System.NotImplementedException();
    }
}