using TMPro;
using UnityEngine;

public class ManaManager : MonoBehaviour {
    public int maxMana = 10;
    public int currentMaxMana;
    public int currentMana;
    public TMP_Text manaCount;

    private void Start() {
        currentMana = 0;
        manaCount.text = "Mana: " + currentMana;
    }

    public void UseMana(CardDisplay cardDisplay) {
        UseMana(cardDisplay.cardData);
    }

    public void UseMana(BaseCardData card) {
        if (CanPlayCard(card)) {
            currentMana -= card.cost;
            manaCount.text = "Mana: " + currentMana;
        }
    }

    public bool CanPlayCard(CardDisplay cardDisplay) {
        return CanPlayCard(cardDisplay.cardData);
    }    
    public bool CanPlayCard(BaseCardData card) {
        return currentMana >= card.cost;
    }
    public void StartRound() {
        if (currentMaxMana < maxMana)
            currentMaxMana++;
        currentMana = currentMaxMana;
        manaCount.text = "Mana: " + currentMana;
    }
}