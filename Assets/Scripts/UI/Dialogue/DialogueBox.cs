using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour {
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text dialogueText;

    private void Awake() {
        HideDialogue();
    }
    
    public void SetDialogue(Dialogue dialogue) {
        icon.sprite = dialogue.Icon;
        nameText.text = dialogue.NameText;
        dialogueText.text = dialogue.DialogueText;
    }

    public void ShowDialogue() {
        gameObject.SetActive(true);
        UIManager.Instance.SetIsOpen(true);
    }
    
    public void HideDialogue() {
        gameObject.SetActive(false);
        UIManager.Instance.SetIsOpen(false);
    }
}
