using UnityEngine;

public class Dialogue : ScriptableObject{
    [TextArea]
    [SerializeField] private string dialogueText;
    
    public string DialogueText => dialogueText;
}