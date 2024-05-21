using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour {
    [SerializeField] private GameObject dialoguePanel;
    private DialogueBox dialogueBox;

    private NPCDialogue currentDialogue;
    private int currentIndex;
    private List<MainDialogue> currentDialogueList = new List<MainDialogue>();
    
    public bool IsOpen { get; private set; }

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
            HUDController.Instance.HideHUD();
            IsOpen = true;
            ShowMainDialogue();
            currentIndex++;
            currentDialogue.SetMainIndex(currentIndex);
        }
    }

    private void ShowMainDialogue() {
        dialoguePanel.SetActive(true);
        dialogueBox.ShowDialogue(currentDialogueList[currentIndex]);
    }
    
    private void CloseDialogue() {
        IsOpen = false;
        HideMainDialogue();
        currentDialogue.SetMainIndex(currentIndex);
        HUDController.Instance.ShowHUD();
    }

    private void HideMainDialogue() {
        dialogueBox.HideDialogue();
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
        currentIndex = currentDialogue.GetMainIndex();
        currentDialogueList = currentDialogue.GetMainDialogues();
        OpenDialogue();
    }
    
}
