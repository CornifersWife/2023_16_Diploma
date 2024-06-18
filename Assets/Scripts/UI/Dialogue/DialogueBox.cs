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

    private const string HTML_ALPHA = "<color=#00000000>";
    
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
        gameObject.SetActive(false);
    }
    
    private void SetDialogue(MainDialogue dialogue) {
        icon.sprite = dialogue.Icon;
        nameText.text = dialogue.NameText;
        sentence = dialogue.DialogueText;
    }

    private IEnumerator TypeSentence() {
        isTyping = true;
        dialogueText.text = "";
        string originalText = sentence;
        string displayedText = "";
        int alphaIndex = 0;
        
        foreach (char letter in sentence) {
            alphaIndex++;
            dialogueText.text = originalText;
            displayedText = dialogueText.text.Insert(alphaIndex, HTML_ALPHA);
            dialogueText.text = displayedText;
            yield return new WaitForSeconds(1/typingSpeed);
        }
        wasSkipped = true;
        nextIcon.SetActive(true);
        //isTyping = false;
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
