using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public DeckManager enemyDeckManager;
    public HandManager enemyHandManager;
    public BoardSideManager boardSideManager;

    private Dictionary<Vector3, bool> positionAvailabilityMap;

    public void Start()
    {
        
    }

    public void PlayCard()
    {
        System.Random random = new System.Random();
        if (enemyHandManager.hand.Count != 0)
        {
            //take random index
            int index = random.Next(0, enemyHandManager.hand.Count);
            BaseCardData playedCard = enemyHandManager.PlayCard(index);
            
            index = random.Next(0, boardSideManager.count);
            boardSideManager.AddCardToBoard(playedCard, index);
        }
    }
}
