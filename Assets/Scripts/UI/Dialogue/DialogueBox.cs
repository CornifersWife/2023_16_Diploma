using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour {
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text dialogueText;
    
    public void SetDialogue(Dialogue dialogue) {
        icon.sprite = dialogue.Icon;
        nameText.text = dialogue.NameText;
        dialogueText.text = dialogue.DialogueText;
    }

    public void ShowDialogue() {
        UIManager.Instance.SetIsOpen(true);
    }
    
    public void HideDialogue() {
        UIManager.Instance.SetIsOpen(false);
    }
}
