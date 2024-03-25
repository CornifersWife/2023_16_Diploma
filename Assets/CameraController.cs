using UnityEngine;

public class CameraController : MonoBehaviour {
    [Header("Zoom")]
    [SerializeField] private float zoomSpeed = 0.1f;
    [Range(1, 10)]
    [SerializeField] private float minZoomRange = 1;
    [Range(1, 10)]
    [SerializeField] private float maxZoomRange = 1;
    
    [Header("Rotate")]
    [SerializeField] private float rotationSpeed = 5f;

    private Camera mainCamera;

    private void Awake() {
        mainCamera = Camera.main;
    }

    private void ZoomIn() {
        if(mainCamera.orthographicSize > minZoomRange)
            mainCamera.orthographicSize -= zoomSpeed;
    }
    
    private void ZoomOut() {
        if(mainCamera.orthographicSize < maxZoomRange)
            mainCamera.orthographicSize += zoomSpeed;
    }

    private void RotateLeft() {
        transform.Rotate(Vector3.down * rotationSpeed * Time.deltaTime, Space.World); 
    }
    
    private void RotateRight() {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }
}
