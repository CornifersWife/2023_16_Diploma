using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManaManager : MonoBehaviour {
    public int actualMana;
    public int usedMana;
    public TMP_Text manaCount;

    //Use at the beginning of the game
    public void Start() {
        usedMana = 0;
        manaCount.text = "Mana: " + actualMana;
    }

    //Use this method to buy a card
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

    //Use this method when starting a new round 
    public void NextRound() {
        actualMana += usedMana;
        usedMana = 0;
        if (actualMana < 10) {
            actualMana++;
        }
        manaCount.text = "Mana: " + actualMana;
    }
}