using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour {
    [SerializeField] private GameObject dialoguePanel;
    private DialogueBox dialogueBox;

    private NPCDialogue currentDialogue;
    private int currentIndex;
    private List<Dialogue> currentDialogueList;

    public static DialogueManager Instance = null;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
        dialogueBox = dialoguePanel.GetComponent<DialogueBox>();
    }

    private void OpenDialogue() {
        if (currentIndex < currentDialogueList.Count) {
            dialoguePanel.SetActive(true);
            dialogueBox.ShowDialogue(currentDialogueList[currentIndex]);
            currentIndex++;
        }
    }
    
    private void CloseDialogue() {
        dialogueBox.HideDialogue();
        currentDialogue.SetIndex(currentIndex);
    }
    
    public void NextDialogue() {
        if (currentIndex < currentDialogueList.Count) {
            if (currentDialogueList[currentIndex].DialogueText == ".") {
                currentIndex++;
                CloseDialogue();
                return;
            }
            dialogueBox.ShowDialogue(currentDialogueList[currentIndex]);
            currentIndex++;
        }
        else {
            CloseDialogue();
        }
    }

    public void SetCurrentDialogue(GameObject dialogueObject) {
        currentDialogue = dialogueObject.GetComponent<NPCDialogue>();
        currentIndex = currentDialogue.GetIndex();
        currentDialogueList = currentDialogue.GetDialogues();
        OpenDialogue();
    }
    
}
