using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour {
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private GameObject nextIcon;
    
    [SerializeField] private float typingSpeed;
    [SerializeField] private InputAction skipDialogue;

    private string sentence;
    private bool wasSkipped;
    private bool isTyping;
    
    private void OnEnable() {
        skipDialogue.Enable();
        skipDialogue.performed += ShowAllText;
    }

    private void OnDisable() {
        skipDialogue.performed -= ShowAllText;
        skipDialogue.Disable();
    }

    public void ShowDialogue(MainDialogue dialogue) {
        isTyping = false;
        nextIcon.SetActive(false);
        InputManager.Instance.DisableAllInput();
        SetDialogue(dialogue);
        StopAllCoroutines();
        StartCoroutine(TypeSentence());
    }
    
    public void HideDialogue() {
        InputManager.Instance.EnableAllInput();
        ObjectClickHandler.Instance.EnableClickDetection();
        gameObject.SetActive(false);
    }
    
    private void SetDialogue(MainDialogue dialogue) {
        icon.sprite = dialogue.Icon;
        nameText.text = dialogue.NameText;
        sentence = dialogue.DialogueText;
    }

    private IEnumerator TypeSentence() {
        dialogueText.text = "";
        foreach (char letter in sentence) {
            dialogueText.text += letter;
            if (dialogueText.text.Length > 1) // set the isTyping when we have at least 2 characters
                isTyping = true;
            yield return new WaitForSeconds(1/typingSpeed);
        }
        nextIcon.SetActive(true);
    }

    private void ShowAllText(InputAction.CallbackContext context) {
        if (!isTyping)
            return;
        
        if (!wasSkipped) {
            StopAllCoroutines();
            dialogueText.text = sentence;
            nextIcon.SetActive(true);
            wasSkipped = true;
        }
        else {
            DialogueManager.Instance.NextDialogue();
            wasSkipped = false;
        }
    }
}
