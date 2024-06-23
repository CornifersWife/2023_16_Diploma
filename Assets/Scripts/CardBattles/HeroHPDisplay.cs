using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[Obsolete]
public class HeroHpDisplay : MonoBehaviour {
    private HeroOld heroOld;
    public Button button; 
    private int showValue;
    private TMP_Text buttonText;

    private void Start() {
        heroOld = GetComponent<HeroOld>();
        showValue = heroOld.currentHealth;
        buttonText = button.GetComponentInChildren<TMP_Text>(); 
    }

    void Update()
    {
        showValue = heroOld.currentHealth;
        UpdateButtonText();
    }

    void UpdateButtonText()
    {
        buttonText.text = showValue.ToString();
    }
}