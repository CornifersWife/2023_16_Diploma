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
    [Range(0, 1)]
    [SerializeField] private float rightRotateRangeUnits = 0.2f;
    [Range(0, 1)]
    [SerializeField] private float leftRotateRangeUnits = 0.2f;

    private Camera mainCamera;
    private float initialRotation;
    private float rightRotation;
    private float leftRotation;

    private static CameraController instance = null;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }         
        else if (instance != this) {
            Destroy(gameObject);
        }          
        DontDestroyOnLoad(gameObject); 
        mainCamera = Camera.main;
        initialRotation = transform.rotation.y;
    }

    private void ZoomIn() {
        if(mainCamera.orthographicSize > minZoomRange)
            mainCamera.orthographicSize -= zoomSpeed;
    }
    
    private void ZoomOut() {
        if(mainCamera.orthographicSize < maxZoomRange)
            mainCamera.orthographicSize += zoomSpeed;
    }

    private void RotateRight() {
        if (rightRotation < rightRotateRangeUnits) {
            transform.Rotate(Vector3.down * (rotationSpeed * Time.deltaTime), Space.World);
            rightRotation = initialRotation - transform.rotation.y;
        }
    }
    
    private void RotateLeft() {
        if (leftRotation < leftRotateRangeUnits) {
            transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime), Space.World);
            leftRotation = transform.rotation.y - initialRotation;
        }
    }
}
