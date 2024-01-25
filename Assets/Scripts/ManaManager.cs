using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManaManager : MonoBehaviour {
    private int actualMana;
    private int usedMana;
    public TextMeshPro manaCount;

    //Use at the beginning of the game
    public void GameStart() {
        manaCount = GetComponent<TextMeshPro>();
        actualMana = 1;
        manaCount.text = "Mana: " + actualMana;
    }

    //Use this method to buy a card
    public bool UseMana(Card card) {
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
        if (actualMana < 10) {
            actualMana++;
        }
        manaCount.text = "Mana: " + actualMana;
    }
}