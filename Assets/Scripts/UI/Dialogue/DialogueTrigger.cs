using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour, PlayerControls.INPCClickMapActions {
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private List<Dialogue> dialogues;

    private PlayerControls playerControls;
    private DialogueBox dialogueBox;
    private Camera mainCamera;
    private LayerMask npcLayer;

    private int index = 0;
    
    private void Start() {
        mainCamera = Camera.main;
        npcLayer = LayerMask.NameToLayer("NPC");
        dialogueBox = dialoguePanel.GetComponent<DialogueBox>();
        
        playerControls = InputManager.Instance.playerControls;
        playerControls.NPCClickMap.SetCallbacks(this);
        playerControls.NPCClickMap.Enable();
    }
    
    public void OnNPCClick(InputAction.CallbackContext context) {
        if(context.started && index < dialogues.Count)
            OpenDialogue();
    }

    private void OpenDialogue() {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider && hit.collider.gameObject.layer.CompareTo(npcLayer) == 0) {
            dialoguePanel.SetActive(true);
            dialogueBox.SetDialogue(dialogues[index]);
            dialogueBox.ShowDialogue();
            ObjectClickHandler.Instance.SetObject(hit.collider.gameObject);
            playerControls.ObjectClickMap.Disable();
            playerControls.PlayerActionMap.Disable();
            index++;
        }
    }

    public void NextDialoguePage() {
        if (index < dialogues.Count) {
            dialogueBox.SetDialogue(dialogues[index]);
            index++;
        }
        else {
            dialogueBox.HideDialogue();
            dialoguePanel.SetActive(false);
        }
    }
}
