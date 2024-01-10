using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public abstract class Card : MonoBehaviour {
    private static int nextId = 0;

    public int id { get; private set; }
    public int cost;
    public string cardName;

    public Card() {
        id = nextId++;
    }
    public Card(string CardName, int Cost) : this() {
        cardName = CardName;
        cost = Cost;
    }
    
    
    
    public Sprite cardImage;
    private SpriteRenderer spriteRenderer;
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && cardImage != null) {
            spriteRenderer.sprite = cardImage;
        }
    }
    
    
    // Other methods for Card
}

[System.Serializable]
public class MinionCard : Card {
    public int power;
    public int health;
    
    public MinionCard() : base() {
    }
    
    public MinionCard(string CardName, int Cost, int Power, int Health) : base(CardName, Cost) {
        power = Power;
        health = Health;
    }

}

[System.Serializable]
public class SpellCard : Card {
    
    public SpellCard() : base() {
    }
    
    public SpellCard(string CardName, int Cost) : base(CardName, Cost) {
        throw new NotImplementedException();
    }
    // Other methods for Card

}
