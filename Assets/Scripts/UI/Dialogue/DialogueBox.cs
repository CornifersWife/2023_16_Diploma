using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour {
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private GameObject nextButton;
    
    [SerializeField] private float typingSpeed;

    public void ShowDialogue(Dialogue dialogue) {
        nextButton.SetActive(false);
        UIManager.Instance.SetIsOpen(true);
        SetDialogue(dialogue);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(dialogue.DialogueText));
    }
    
    public void HideDialogue() {
        UIManager.Instance.SetIsOpen(false);
        gameObject.SetActive(false);
    }
    
    private void SetDialogue(Dialogue dialogue) {
        icon.sprite = dialogue.Icon;
        nameText.text = dialogue.NameText;
    }

    private IEnumerator TypeSentence(string sentence) {
        dialogueText.text = "";
        foreach (char letter in sentence) {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        nextButton.SetActive(true);
    }
}
