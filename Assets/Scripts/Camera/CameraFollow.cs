using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [SerializeField] private float smoothing = 5f;
    [SerializeField] private Transform target;

    private Vector3 offset;
    private Vector3 cameraPosition;
    private Vector3 targetPosition;

    private void Awake() {
        cameraPosition = transform.position;
        targetPosition = target.position;
        cameraPosition = targetPosition;
    }

    private void Start() {
        offset = cameraPosition - targetPosition;
    }

    private void Update() {
        Vector3 newPosition = target.position + offset;
        transform.position = Vector3.Lerp (transform.position, newPosition, smoothing * Time.deltaTime);
    }
}
