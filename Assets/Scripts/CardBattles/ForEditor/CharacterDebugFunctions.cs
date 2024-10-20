using System;
using System.Collections;
using System.Collections.Generic;
using CardBattles.Character;
using NaughtyAttributes;
using UnityEngine;
[RequireComponent(typeof(CharacterManager))]
public class CharacterDebugFunctions : MonoBehaviour {
    private CharacterManager characterManager;

    private void Awake() {
        characterManager = GetComponent<CharacterManager>();
    }

    [Button]
    private void AddCardToDeck() {
        characterManager.deck.InitializeDeck();
    }

    private void ClearDeck() {
        
    }

    private void DrawACard() {
        
    }

    private void KillAllMinions() {
        
    }
}
