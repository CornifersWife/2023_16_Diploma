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
    private bool skipped;
    
    private void OnEnable() {
        skipDialogue.Enable();
        skipDialogue.performed += ShowAllText;
    }

    private void OnDisable() {
        skipDialogue.performed -= ShowAllText;
        skipDialogue.Disable();
    }

    public void ShowDialogue(MainDialogue dialogue) {
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
            yield return new WaitForSeconds(1/typingSpeed);
        }
        nextIcon.SetActive(true);
    }

    private void ShowAllText(InputAction.CallbackContext context) {
        if (!skipped) {
            StopAllCoroutines();
            dialogueText.text = sentence;
            nextIcon.SetActive(true);
            skipped = true;
        }
        else {
            DialogueManager.Instance.NextDialogue();
            skipped = false;
        }
    }
}
