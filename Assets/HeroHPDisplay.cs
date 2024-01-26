using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroHPDisplay : MonoBehaviour {
    private Hero hero;
    public Button button; // Assign your button here
    private int showValue;
    private int maxValue;// Your int variable

    private void Start() {
        hero = GetComponent<Hero>();
        maxValue = hero.maxHealth;
        showValue = hero.currentHealth;
    }

    void Update()
    {
        showValue = hero.currentHealth;
        UpdateButtonText();
    }

    void UpdateButtonText()
    {
        TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>(); 
        buttonText.text = showValue.ToString();
    }
}