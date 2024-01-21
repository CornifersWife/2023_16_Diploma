using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public DeckManager enemyDeckManager;
    public HandManager enemyHandManager;
    public CardPositionManager cardPositionManager;

    public void PlayCard()
    {
        System.Random random = new System.Random();
        List<CardDisplay> cards = enemyHandManager.hand;
        CardDisplay playedCard = cards[random.Next(0, cards.Count)];

        playedCard.transform.position =
            cardPositionManager._opponentPositions[random.Next(0, cardPositionManager._opponentPositions.Count)];
    }
}
