using Interaction;
using ScriptableObjects.Dialogue;
using UnityEngine;

namespace NPC {
    public class TalkableNPC : NPC, ITalkable {
        [SerializeField] private DialogueText dialogueText;
        [SerializeField] private DialogueController dialogueController;
        public override void Interact() {
            Talk(dialogueText);
        }

        public void Talk(DialogueText dialogueText) {
            dialogueController.DisplaySentence(dialogueText);
        }
    }
}