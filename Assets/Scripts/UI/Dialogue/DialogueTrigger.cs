using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DialogueTrigger : MonoBehaviour {
    [SerializeField] private InputAction mouseClickAction;
    
    private Camera mainCamera;
    private LayerMask npcLayer;
    
    private void Start() {
        mainCamera = Camera.main;
        npcLayer = LayerMask.NameToLayer("NPC");
    }
    
    private void OnEnable() {
        mouseClickAction.Enable();
        mouseClickAction.performed += OpenDialogue;
    }

    private void OnDisable() {
        mouseClickAction.performed -= OpenDialogue;
        mouseClickAction.Disable();
    }

    private void OpenDialogue(InputAction.CallbackContext context) {
        if (PauseManager.Instance.IsOpen || DialogueManager.Instance.IsOpen)
            return;

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Irys playspace")) 
            return;
        
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider && hit.collider.gameObject.layer.CompareTo(npcLayer) == 0) {
            GameObject NPC = hit.collider.gameObject;
            DialogueManager.Instance.SetCurrentDialogue(NPC);
            ObjectClickHandler.Instance.DisableClickDetection();
        }
    }
}