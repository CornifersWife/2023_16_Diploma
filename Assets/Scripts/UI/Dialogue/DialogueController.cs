using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Dialogue;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour, IPointerClickHandler{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private GameObject nextIcon;
    
    [SerializeField] private float typingSpeed;

    private const string HTML_ALPHA = "<color=#00000000>";

    private Queue<string> sentences = new Queue<string>();
    private string sentence;
    private DialogueText dialogue;
    
    private bool wasSkipped;
    private bool isTyping;
    private bool conversationEnded;
    public bool ConversationEnded => conversationEnded;

    public static DialogueController Instance;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    public void DisplaySentence(DialogueText dialogue) {
        this.dialogue = dialogue;
        
        if (sentences.Count == 0) {
            if (!conversationEnded) {
                StartConversation(dialogue);
            }
            else if (conversationEnded && !isTyping) {
                EndConversation();
                return;
            }
        }

        if (!isTyping) {
            sentence = sentences.Dequeue();
            dialogueText.text = sentence;
            ShowDialogue();
        }
        else {
            ShowAllText();
        }
        
        if (sentences.Count == 0) {
            conversationEnded = true;
        }
    }

    private void StartConversation(DialogueText dialogue) {
        if (!gameObject.activeSelf) {
            gameObject.SetActive(true);
        }
        SetDialogue(dialogue);
        HUDController.Instance.HideHUD();
    }

    private void EndConversation() {
        conversationEnded = false;
        sentences.Clear();
        HideDialogue();
        HUDController.Instance.ShowHUD();
    }
    
    private void ShowDialogue() {
        isTyping = false;
        nextIcon.SetActive(false);
        InputManager.Instance.DisableAllInput();
        StopAllCoroutines();
        StartCoroutine(TypeSentence());
    }
    
    private void HideDialogue() {
        InputManager.Instance.EnableAllInput();
        gameObject.SetActive(false);
    }
    
    private void SetDialogue(DialogueText dialogue) {
        icon.sprite = dialogue.Icon;
        nameText.text = dialogue.NameText;
        foreach (string sentence in dialogue.Sentences) {
            sentences.Enqueue(sentence);
        }
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
        nextIcon.SetActive(true);
        isTyping = false;
    }

    private void ShowAllText() {
        StopAllCoroutines();
        dialogueText.text = sentence;
        nextIcon.SetActive(true);
        isTyping = false;
    }
    public void OnPointerClick(PointerEventData eventData) {
        DisplaySentence(dialogue);
    }
}
