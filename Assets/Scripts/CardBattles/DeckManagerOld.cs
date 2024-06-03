using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class DeckManagerOld : MonoBehaviour {
    [SerializeField] private bool isPlayer;
    [SerializeField] private List<CardSetData> cardSets = new List<CardSetData>();
    [Space(10)]
    public List<BaseCardData> deck = new List<BaseCardData>();

    private void Awake() {
        //if(!Application.isEditor)
            cardSets = LoadCardSetData();
        deck = CreateDeckFromCardSets();
    }

    private List<BaseCardData> CreateDeckFromCardSets() {
        var cardSetDataCopies = new List<CardSetData>();
        foreach (CardSetData cardSet in cardSets) {
            cardSetDataCopies.Add(ScriptableObject.Instantiate(cardSet));
        }

        var baseCardDatas = new List<BaseCardData>();
        Debug.Log("FIX ME I USED TO BE BASECARDDATA");
        // foreach (CardSetData cardSet in cardSetDataCopies) {
        //     baseCardDatas.AddRange(cardSet.cards);
        // }

        foreach (var card in baseCardDatas) {
            card.belongsToPlayer = isPlayer;
        }

        return baseCardDatas;
    }

    private List<CardSetData> LoadCardSetData() {
        if(isPlayer)
            return InventoryController.Instance.GetCardSets();
        return EnemyStateManager.Instance.GetCurrentEnemy().GetDeck();
    }

    public BaseCardData DrawCard() {
        if (deck.Count == 0) return null;

        BaseCardData cardData = deck[0];
        deck.RemoveAt(0);
        return cardData;
    }

    // Fisher-Yates shuffle
    public void Shuffle() {
        System.Random rand = new System.Random();
        int n = deck.Count;

        for (int i = 0; i < n - 1; i++) {
            int r = i + rand.Next(n - i);

            (deck[r], deck[i]) = (deck[i], deck[r]);
        }
    }
}