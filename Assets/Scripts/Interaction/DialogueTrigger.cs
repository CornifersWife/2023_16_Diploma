using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour, PlayerControls.INPCClickMapActions {

    private PlayerControls playerControls;
    private Camera mainCamera;
    private LayerMask npcLayer;
    
    private void Start() {
        mainCamera = Camera.main;
        npcLayer = LayerMask.NameToLayer("NPC");
        
        playerControls = InputManager.Instance.playerControls;
        playerControls.NPCClickMap.SetCallbacks(this);
        playerControls.NPCClickMap.Enable();
    }
    
    public void OnNPCClick(InputAction.CallbackContext context) {
        if(context.started)
            OpenDialogue();
    }

    private void OpenDialogue() {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider && hit.collider.gameObject.layer.CompareTo(npcLayer) == 0) {
            GameObject NPC = hit.collider.gameObject;
            NPCDialogue npcDialogue = NPC.GetComponent<NPCDialogue>();
            DialogueManager.Instance.SetCurrentDialogue(npcDialogue.GetIndex(), npcDialogue.GetDialogues());
            ObjectClickHandler.Instance.SetObject(NPC);
            playerControls.ObjectClickMap.Disable();
            playerControls.PlayerActionMap.Disable();
        }
    }
}