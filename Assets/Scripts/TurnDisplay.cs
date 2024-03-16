
using TMPro;
using TurnSystem;
using UnityEngine;
using UnityEngine.UI;



public class TurnDisplay : MonoBehaviour {
    private TurnManager turnManager;
    public Button button;
    private TMP_Text buttonText;
    private Image buttonColor;
    private void Start() {
        turnManager = TurnManager.Instance;
        buttonText = button.GetComponentInChildren<TMP_Text>();
        buttonColor = button.GetComponent<Image>();
    }

    private void Update() {
        UpdateButtonText();
    }
    void UpdateButtonText()
    {
        buttonText.text = turnManager.isPlayerTurn ? "Your Turn" : "Enemy Turn";
        buttonColor.color =
            turnManager.isPlayerTurn ? new Color(0.8f, 1f, 0.6f, 1f) : new Color(1f, 0.5f, 0.5f, 1f);
    }
}
