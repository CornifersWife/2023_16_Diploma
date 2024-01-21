using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableCard : MonoBehaviour {
    public Camera mainCamera;
    public bool isDragging = false;
    public Vector3 offset;
    public float distanceToCamera;
    public Vector3 dragStartScreenPosition;
    private Vector3 originalPosition;
    public CardPositionManager cardPositionManager;
    
    void Start() {
        mainCamera = Camera.main; // Cache the main camera
        originalPosition = transform.position;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.transform == this.transform) {
                isDragging = true;
                dragStartScreenPosition = Input.mousePosition;
                distanceToCamera = Vector3.Distance(transform.position, mainCamera.transform.position);
                offset = transform.position - mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToCamera));
            }
        }

        if (isDragging) {
            Vector3 currentScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToCamera);
            Vector3 currentPosition = mainCamera.ScreenToWorldPoint(currentScreenPosition) + offset;
            transform.position = currentPosition;
        }

        if (Input.GetMouseButtonUp(0) && isDragging) {
            isDragging = false;
            // Logic to check if the card is dropped over a valid spot
            // If not, reset position
        }
    }

    public void OnPointerUp(PointerEventData eventData) {
        isDragging = false;
        if (IsOverValidSpot(out Vector3 validSpot)) {
            PlaceCardAt(validSpot);
        } else {
            ResetCardPosition();
        }
    }
    
    private bool IsOverValidSpot(out Vector3 validSpot) {
        // Implement logic to check if the card is over a valid spot
        // This can be done via raycasting or distance checks
        // For example:

        float minDistance = float.MaxValue;
        validSpot = originalPosition;
        bool foundValidSpot = false;

        foreach (var spot in cardPositionManager._playerPositions) {
            float distance = Vector3.Distance(transform.position, spot);
            if (distance < minDistance && distance < 0.5f/*someThresholdDistance*/) {  //add theshold disance as a parameter
                minDistance = distance;
                validSpot = spot;
                foundValidSpot = true;
            }
        }

        return foundValidSpot;
    }
    private void PlaceCardAt(Vector3 position) {
        transform.position = position;
        // Additional logic for when the card is placed (e.g., update game state)
    }

    private void ResetCardPosition() {
        transform.position = originalPosition;
        // Additional logic if needed when the card returns to its original position
    }
    
    // Rest of the code...
}