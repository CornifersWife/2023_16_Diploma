using System.Collections;
using TMPro;
using TurnSystem;
using UnityEngine;
using UnityEngine.UI;

public class TurnDisplay : MonoBehaviour {
    private TurnManager turnManager;
    public Button button;
    private TMP_Text buttonText;
    private Image buttonColor;
    private bool lastTurnState;
    [SerializeField] private float animationDuration = 0.2f;
    
    private void Start() {
        turnManager = TurnManager.Instance;
        buttonText = button.GetComponentInChildren<TMP_Text>();
        buttonColor = button.GetComponent<Image>();
        lastTurnState = turnManager.isPlayerTurn;
        UpdateButtonText();
    }

    private void Update() {
        if (lastTurnState != turnManager.isPlayerTurn) {
            UpdateButtonText();
            lastTurnState = turnManager.isPlayerTurn;
        }
    }

    void UpdateButtonText() {
        buttonText.text = turnManager.isPlayerTurn ? "Your Turn" : "Enemy Turn";

        // Start the animation coroutine
        StartCoroutine(AnimateTurnChange());
    }

    public IEnumerator AnimateTurnChange() {
        Color startColor = buttonColor.color;
        Color playerTurnColor = new Color(0.8f, 1f, 0.6f, 1f);
        Color enemyTurnColor = new Color(1f, 0.5f, 0.5f, 1f);
        Color endColor = turnManager.isPlayerTurn ? playerTurnColor : enemyTurnColor;

        float time = 0;


        while (time < animationDuration) {
            buttonColor.color = Color.Lerp(startColor, endColor, time / animationDuration);

            time += Time.deltaTime;
            yield return null;
        }

        buttonColor.color = endColor;
    }
}