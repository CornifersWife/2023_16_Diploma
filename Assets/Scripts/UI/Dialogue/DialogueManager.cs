using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour {
    [SerializeField] private GameObject dialoguePanel;
    //[SerializeField] private DialogueBox dialogueBox;

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
        //dialogueBox.ShowDialogue(currentDialogueList[currentIndex]);
    }
    
    private void CloseDialogue() {
        IsOpen = false;
        HideMainDialogue();
        currentDialogue.SetMainIndex(currentIndex);
        HUDController.Instance.ShowHUD();
        ManageGame.Instance.IsAfterTutorial = true;
    }

    private void HideMainDialogue() {
        //dialogueBox.HideDialogue();
        dialoguePanel.SetActive(false);
    }
    
    public void NextDialogue() {
        if (currentIndex < currentDialogueList.Count) {
            if (currentDialogueList[currentIndex].DialogueText == ".") {
                currentIndex++;
                CloseDialogue();
                return;
            }
            //dialogueBox.ShowDialogue(currentDialogueList[currentIndex]);
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
