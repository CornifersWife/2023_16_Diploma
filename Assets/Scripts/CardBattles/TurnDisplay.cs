/*
using System;
using TMPro;
using TurnSystem;
using UnityEngine;
using UnityEngine.UI;


[Obsolete]
public class TurnDisplay : MonoBehaviour {
    private TurnManagerOld turnManagerOld;
    public Button button;
    private TMP_Text buttonText;
    private Image buttonColor;
    private void Start() {
        turnManagerOld = TurnManagerOld.Instance;
        buttonText = button.GetComponentInChildren<TMP_Text>();
        buttonColor = button.GetComponent<Image>();
    }

    private void Update() {
        UpdateButtonText();
    }
    void UpdateButtonText()
    {
        buttonText.text = turnManagerOld.isPlayerTurn ? "Your Turn" : "Enemy Turn";
        buttonColor.color =
            turnManagerOld.isPlayerTurn ? new Color(0.8f, 1f, 0.6f, 1f) : new Color(1f, 0.5f, 0.5f, 1f);
    }
}
*/
