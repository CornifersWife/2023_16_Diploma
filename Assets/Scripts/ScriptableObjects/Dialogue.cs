using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "UI")]
public class Dialogue : ScriptableObject {
    [SerializeField] private Sprite icon;
    [SerializeField] private string nameText;
    [TextArea]
    [SerializeField] private string dialogueText;

    public Sprite Icon => icon;
    public string NameText => nameText;
    public string DialogueText => dialogueText;
}