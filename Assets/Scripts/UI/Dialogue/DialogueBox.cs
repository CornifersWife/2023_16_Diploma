using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour {
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private GameObject nextButton;
    
    [SerializeField] private float typingSpeed;
    [SerializeField] private InputAction mouseClick;

    private string sentence;
    
    private void OnEnable() {
        mouseClick.Enable();
        mouseClick.performed += ShowAllText;
    }

    private void OnDisable() {
        mouseClick.performed -= ShowAllText;
        mouseClick.Disable();
    }

    public void ShowDialogue(MainDialogue dialogue) {
        nextButton.SetActive(false);
        InputManager.Instance.DisableAllInput();
        SetDialogue(dialogue);
        StopAllCoroutines();
        StartCoroutine(TypeSentence());
    }
    
    public void HideDialogue() {
        InputManager.Instance.EnableAllInput();
        ObjectClickHandler.Instance.IsActive = true;
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
        nextButton.SetActive(true);
    }

    private void ShowAllText(InputAction.CallbackContext context) {
        StopAllCoroutines();
        dialogueText.text = sentence;
        nextButton.SetActive(true);
    }
}
