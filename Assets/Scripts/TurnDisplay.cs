
using TMPro;
using TurnSystem;
using UnityEngine;
using UnityEngine.UI;



public class TurnDisplay : MonoBehaviour {
    private TurnManager _turnManager;
    public Button button;
    public bool isPlayerTurn;
    private TMP_Text buttonText;
    private Image buttonColor;
    private void Start() {
        _turnManager = GetComponent<TurnManager>();
        buttonText = button.GetComponentInChildren<TMP_Text>();
        buttonColor = button.GetComponent<Image>();
    }

    private void Update() {
        isPlayerTurn = _turnManager.isPlayerTurn;
        UpdateButtonText();
    }
    void UpdateButtonText()
    {
        buttonText.text = isPlayerTurn ? "Your Turn" : "Enemy Turn";
        buttonColor.color =
            isPlayerTurn ? new Color(0.8f, 1f, 0.6f, 1f) : new Color(1f, 0.5f, 0.5f, 1f);
    }
}
