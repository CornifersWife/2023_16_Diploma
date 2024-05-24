using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class KeyboardInputManager : MonoBehaviour, PlayerControls.IPlayerActionMapActions {
    [SerializeField] private GameObject player;
    [SerializeField] private Transform cameraPivot;
    
    private PlayerControls playerControls;
    private NavMeshAgent navMeshAgent;
    private float speed;
    private Vector3 movementVector;
    private Vector3 targetDirection;
    private float lerpTime;

    private Matrix4x4 rotationMatrix;

    private void Awake() {
        navMeshAgent = player.GetComponent<NavMeshAgent>();
        speed = navMeshAgent.speed;
        playerControls = InputManager.Instance.playerControls;
        playerControls.PlayerActionMap.SetCallbacks(this);
        playerControls.PlayerActionMap.Enable();
        rotationMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 0, -cameraPivot.rotation.eulerAngles.y));
    }
    
    private void Update() {
        if (!ManageGame.Instance.IsStarted)
            return;
        movementVector.Normalize();
        targetDirection = Vector3.Lerp(targetDirection, movementVector, speed * Time.deltaTime);
        navMeshAgent.Move(targetDirection * (navMeshAgent.speed * Time.deltaTime));
        Vector3 lookDirection = movementVector;
        
        if (lookDirection != Vector3.zero) {
            player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.LookRotation(lookDirection),
                speed * Time.deltaTime);
        }
    }
    
    public void OnMove(InputAction.CallbackContext context) {
        navMeshAgent.ResetPath();
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
        if (PauseManager.Instance.IsOpen)
            PauseManager.Instance.Close();
        else {
            PauseManager.Instance.Open();
        }
    }

    public void EnableKeyboardControls() {
        playerControls.PlayerActionMap.Enable();
    }
    
    public void DisableKeyboardControls() {
        playerControls.PlayerActionMap.Disable();
    }
}