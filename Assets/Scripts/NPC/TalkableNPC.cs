using System.Collections.Generic;
using Audio;
using Interaction;
using ScriptableObjects.Dialogue;
using UnityEngine;

namespace NPC {
    public class TalkableNPC : NPC, ITalkable {
        [SerializeField] private List<DialogueText> dialogueText;
        [SerializeField] private DialogueController dialogueController;
        [SerializeField] private ShowQuestIndicator questIndicator;
        [SerializeField] private DialogueAudioConfig audioConfig;
        
        public override void Interact() {
            Talk(dialogueText[0]);
        }

        public void Talk(DialogueText dialogueText) {
            dialogueController.DisplaySentence(dialogueText);
            dialogueController.SetCurrentAudioConfig(audioConfig);
            questIndicator.HideQuestIcon();
        }

        public void SetUpNextDialogue() {
            dialogueText.Remove(dialogueText[0]);
            questIndicator.ShowQuestIcon();
        }
    }
}