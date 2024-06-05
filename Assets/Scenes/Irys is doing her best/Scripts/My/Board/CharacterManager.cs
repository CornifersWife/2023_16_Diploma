using System.Collections;
using System.Collections.Generic;
using Scenes.Irys_is_doing_her_best.Scripts.My;
using Scenes.Irys_is_doing_her_best.Scripts.My.Board;
using UnityEngine;

public class CharacterManager : MonoBehaviour {
    [Header("Data")]
    public HandManager hand;
    public DeckManager deck;
    public Hero hero;
    public BoardSide boardSide;

    
    public void Draw() {
        hand.cards.Add(deck.cards[0]);
        deck.cards.RemoveAt(0);
    }
}
