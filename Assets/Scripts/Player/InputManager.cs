using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour, PlayerControls.IPlayerActionMapActions {

    private PlayerControls playerControls;
    private NavMeshAgent navMeshAgent;
    
    private Vector3 movementVector;
    private Vector3 targetDirection;
    private float lerpTime;

    private Matrix4x4 rotationMatrix;

    [SerializeField] private Transform cameraPivot;
    [SerializeField] private float smoothing;
    
    private void Awake() {
        playerControls = new PlayerControls();
        playerControls.PlayerActionMap.SetCallbacks(this);
        playerControls.PlayerActionMap.Enable();

        navMeshAgent = GetComponent<NavMeshAgent>();
        rotationMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 0, -cameraPivot.rotation.eulerAngles.y));
    }
    
    private void Update() {
        movementVector.Normalize();
        targetDirection = Vector3.Lerp(targetDirection, movementVector, smoothing * Time.deltaTime);
        navMeshAgent.Move(targetDirection * (navMeshAgent.speed * Time.deltaTime));
        Vector3 lookDirection = movementVector;
        
        if (lookDirection != Vector3.zero) {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookDirection),
                smoothing * Time.deltaTime);
        }
    }
    
    public void OnMove(InputAction.CallbackContext context) {
        Vector2 input = context.ReadValue<Vector2>();
        Vector3 skewedInput = rotationMatrix.MultiplyPoint3x4(input);
        movementVector = new Vector3(skewedInput.x, 0, skewedInput.y);
    }
    
    public void OnOpenInventory(InputAction.CallbackContext context) {
        InventoryController inventoryController = InventoryController.Instance;
        if(inventoryController.IsOpen())
            inventoryController.HideInventory();
        else {
            inventoryController.ShowInventory(context);
        }
    }
    
    public void OnPause(InputAction.CallbackContext context) {
        throw new System.NotImplementedException();
    }
}