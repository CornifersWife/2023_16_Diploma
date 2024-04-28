using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour {
    [SerializeField] private GameObject dialoguePanel;
    private DialogueBox dialogueBox;
    
    private List<Dialogue> currentDialogue;
    private int currentIndex;

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
        dialoguePanel.SetActive(true);
        dialogueBox.SetDialogue(currentDialogue[currentIndex]);
        dialogueBox.ShowDialogue();
        currentIndex++;
    }
    
    public void NextDialogue() {
        if (currentIndex < currentDialogue.Count) {
            dialogueBox.SetDialogue(currentDialogue[currentIndex]);
            currentIndex++;
        }
        else {
            dialogueBox.HideDialogue();
            dialoguePanel.SetActive(false);
        }
    }

    public void SetCurrentDialogue(int index, List<Dialogue> dialogueList) {
        currentIndex = index;
        currentDialogue = dialogueList;
        OpenDialogue();
    }

    public List<Dialogue> GetCurrentDialogue() {
        return currentDialogue;
    }
}
