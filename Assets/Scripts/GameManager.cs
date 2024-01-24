using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public DeckManager deckManager;
    public HandManager handManager;
    public BoardSideManager boardSideManager;
    // Call this method to test drawing a card
    public void TestDrawCard() {
        BaseCardData drawnCard = deckManager.DrawCard();
        if (drawnCard != null) {
            handManager.AddCardToHand(drawnCard);
        }
    }


    // Call this method to test shuffling the deck
    public void TestShuffle()
    {
        string ids = "Before shuffle: [";
        foreach (BaseCardData card in deckManager.deck)
        {
            ids += card.id + ", ";
        }
        ids += "]";
        Debug.Log(ids);
        
        deckManager.Shuffle();
        
        ids = "After shuffle: [";
        foreach (BaseCardData card in deckManager.deck)
        {
            ids += card.id + ", ";
        }
        ids += "]";
        Debug.Log(ids);

    public void TestPlayCard() {
        int handIndex = 0;
        BaseCardData playedCard = handManager.PlayCard(handIndex);
        if (playedCard != null) {
            for (int i = 0; i < 5; i++) {
                if (boardSideManager.board[i] == null) {
                    boardSideManager.AddCardToBoard(playedCard, i);
                    break;
                }
            }
        }

    }
}
