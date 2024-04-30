using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour {
    [SerializeField] private GameObject dialoguePanel;
    private DialogueBox dialogueBox;

    private NPCDialogue currentDilogue;
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
    
    public void NextDialogue() {
        if (currentIndex < currentDialogueList.Count) {
            dialogueBox.ShowDialogue(currentDialogueList[currentIndex]);
            currentIndex++;
        }
        else {
            dialogueBox.HideDialogue();
            dialoguePanel.SetActive(false);
            currentDilogue.SetIndex(currentIndex);
        }
    }

    public void SetCurrentDialogue(GameObject dialogueObject) {
        currentDilogue = dialogueObject.GetComponent<NPCDialogue>();
        currentIndex = currentDilogue.GetIndex();
        currentDialogueList = currentDilogue.GetDialogues();
        OpenDialogue();
    }
    
}
