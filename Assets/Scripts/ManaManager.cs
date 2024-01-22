using System;
using UnityEngine;

public class ManaManager : MonoBehaviour {
    private int actualMana;
    private int usedMana;

    //Use at the beginning of the game
    public void GameStart() {
        actualMana = 1;
    }

    //Use this method to buy a card
    public void UseMana(Card card) {
        if (actualMana >= card.cost) {
            actualMana -= card.cost;
            usedMana += card.cost;
        }else {
            throw new NotImplementedException(); 
        }
    }

    //Use this method when starting a new round 
    public void NextRound() {
        actualMana += usedMana;
        if (actualMana < 10) {
            actualMana++;
        }
    }
}