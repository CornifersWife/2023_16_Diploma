using NPC;
using UnityEngine;

public class ChangeDialogueInteract : InteractableObject {
    [SerializeField] private TalkableNPC npc;
    public override void Interact() {
        npc.SetUpNextDialogue();
    }
}
