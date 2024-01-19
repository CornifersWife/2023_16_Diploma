using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour {
    public List<BaseCardData> deck = new List<BaseCardData>();

    // Methods to shuffle, draw, and manage the deck
    
    public BaseCardData DrawCard() {
        if (deck.Count == 0) return null;
    
        BaseCardData cardData = deck[0];
        deck.RemoveAt(0);
        return cardData;
    }

    // Fisher-Yates shuffle
    public void Shuffle()
    {
        System.Random rand = new System.Random();
        int n = deck.Count;
         
        for (int i = 0; i < n-1; i++)
        {
            // Random for remaining positions.
            int r = i + rand.Next(n - i);
             
            //swapping the elements
            BaseCardData temp = deck[r];
            deck[r] = deck[i];
            deck[i] = temp;
             
        }
    }
}
