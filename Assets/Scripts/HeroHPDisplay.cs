using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroHpDisplay : MonoBehaviour {
    private Hero hero;
    public Button button; 
    private int showValue;
    private TMP_Text buttonText;

    private void Start() {
        hero = GetComponent<Hero>();
        showValue = hero.currentHealth;
        buttonText = button.GetComponentInChildren<TMP_Text>(); 
    }

    void Update()
    {
        showValue = hero.currentHealth;
        UpdateButtonText();
    }

    void UpdateButtonText()
    {
        buttonText.text = showValue.ToString();
    }
}