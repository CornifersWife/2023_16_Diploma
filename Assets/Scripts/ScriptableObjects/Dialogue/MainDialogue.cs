using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue/Main Dialogue")]
public class MainDialogue : Dialogue {
    [SerializeField] private Sprite icon;
    [SerializeField] private string nameText;

    public Sprite Icon => icon;
    public string NameText => nameText;
}