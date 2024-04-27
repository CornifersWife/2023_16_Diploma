using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectClickHandler : MonoBehaviour {
    [SerializeField] private InputAction clickAction;

    public static ObjectClickHandler Instance = null;

    private Camera mainCamera;
    private GameObject clickedObject;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        else {
            Instance = this;
        }

        mainCamera = Camera.main;
    }

    private void OnEnable() {
        clickAction.Enable();
        clickAction.performed += SetObject;
    }

    private void OnDisable() {
        clickAction.performed -= SetObject;
        clickAction.Disable();
    }

    private void SetObject(InputAction.CallbackContext context) {
        Ray ray = mainCamera.ScreenPointToRay( Input.mousePosition );
		
        if(Physics.Raycast( ray, out RaycastHit hit) && !UIManager.Instance.IsOpen()) {
            clickedObject = hit.collider.gameObject;
        }
    }
    
    public GameObject GetObject() {
        return clickedObject;
    }
}