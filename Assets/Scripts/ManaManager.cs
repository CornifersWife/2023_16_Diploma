using TMPro;
using UnityEngine;

public class ManaManager : MonoBehaviour {
    public int maxMana = 3;
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
        currentMana = maxMana;
        manaCount.text = "Mana: " + currentMana;
    }
}