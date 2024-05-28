using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubble : MonoBehaviour {
    [SerializeField] private GameObject speechBubble;
    [SerializeField] private TMP_Text speechText;
    [SerializeField] private float offset = 2f;

    [Space] [SerializeField] private GameObject host;
    
    [Space]
    [SerializeField] private GameObject player;
    [SerializeField] private float detectionRadius = 3f;

    private NPCDialogue NPCdialogue;
    private ShortDialogue dialogue;

    private void Awake() {
        NPCdialogue = host.GetComponent<NPCDialogue>();
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update() {
        dialogue = NPCdialogue.GetShortDialogue();
        if (dialogue is not null && DetectPlayer()) {
            UpdatePosition();
            SetUpBubble();
            StartCoroutine(ShowBubble());
        }
    }

    private void UpdatePosition() {
        transform.position = host.transform.position + new Vector3(0, offset, 0);
    }

    private void SetUpBubble() {
        speechBubble.SetActive(true);
        speechText.text = dialogue.DialogueText;
    }

    private IEnumerator ShowBubble() {
        yield return new WaitForSeconds(NPCdialogue.GetShortDialogue().Seconds);
        HideBubble();
    }

    private void HideBubble() {
        speechBubble.SetActive(false);
    }

    public void IncreaseIndex() {
        NPCdialogue.SetShortIndex(NPCdialogue.GetShortIndex()+1);
    }
    
    private bool DetectPlayer() {
        float distanceToPlayer = Vector3.Distance(host.transform.position, player.transform.position);
        return distanceToPlayer <= detectionRadius;
    }
}
