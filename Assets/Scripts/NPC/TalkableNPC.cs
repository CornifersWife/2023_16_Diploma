using System.Collections.Generic;
using Audio;
using Interaction;
using ScriptableObjects.Dialogue;
using UnityEngine;
using UnityEngine.Serialization;

namespace NPC {
    public class TalkableNPC : NPC, ITalkable {
        [SerializeField] private List<DialogueText> dialogueText;
        [SerializeField] private DialogueController dialogueController;
        [SerializeField] private ShowQuestIndicator questIndicator;
        [SerializeField] private DialogueAudioConfig audioConfig;
        [Range(0, 10)] 
        [SerializeField] private float detectionDistance = 8;

        private GameObject player;

        private void Awake() {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        
        public override void Interact() {
            if (Vector3.Distance(player.transform.position, transform.position) < detectionDistance) {
                Talk(dialogueText[0]);
            }
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