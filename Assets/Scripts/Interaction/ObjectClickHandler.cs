using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectClickHandler : MonoBehaviour, PlayerControls.IObjectClickMapActions {
    public static ObjectClickHandler Instance = null;
    
    private PlayerControls playerControls;

    private Camera mainCamera;
    private GameObject clickedObject;

    public bool IsActive {
        get;
        set;
    }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        else {
            Instance = this;
        }
    }

    private void Start() {
        playerControls = InputManager.Instance.playerControls;
        playerControls.ObjectClickMap.SetCallbacks(this);
        playerControls.ObjectClickMap.Enable();
        mainCamera = Camera.main;
    }
    
    public void OnObjectClick(InputAction.CallbackContext context) {
        if(context.started)
            SetObject();
    }

    private void SetObject() {
        if (IsActive) {
            Ray ray = mainCamera.ScreenPointToRay( Input.mousePosition );
		
            if(Physics.Raycast( ray, out RaycastHit hit)) {
                clickedObject = hit.collider.gameObject;
            } 
        }
    }

    public void SetObject(GameObject newObject) {
        clickedObject = newObject;
    }
    
    public GameObject GetObject() {
        return clickedObject;
    }
}