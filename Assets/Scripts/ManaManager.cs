using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManaManager : MonoBehaviour {
    public int actualMana;
    public int usedMana;
    public TMP_Text manaCount;

    public void Start() {
        usedMana = 0;
        manaCount.text = "Mana: " + actualMana;
    }

    public bool UseMana(BaseCardData card) {
        if (actualMana >= card.cost) {
            actualMana -= card.cost;
            usedMana += card.cost;
            manaCount.text = "Mana: " + actualMana;
            return true;
        }else {
            return false;
        }
    }

    public void NextRound() {
        actualMana += usedMana;
        usedMana = 0;
        if (actualMana < 10) {
            actualMana++;
        }
        manaCount.text = "Mana: " + actualMana;
    }
}