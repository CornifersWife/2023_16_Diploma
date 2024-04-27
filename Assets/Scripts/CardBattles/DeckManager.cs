using System;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour {
    [Header("CardSets")]
    [SerializeField] private List<CardSetData> cardSets = new List<CardSetData>();
    [Space(10)]
    public List<BaseCardData> deck = new List<BaseCardData>();

    private void Awake() {
        cardSets = LoadCardSetData();
    }

    private void Start() {
        deck = CreateDeckFromCardSets();
    }

    private List<BaseCardData> CreateDeckFromCardSets() {
        var cardSetDataCopies = new List<CardSetData>();
        foreach (CardSetData cardSet in cardSets) {
            cardSetDataCopies.Add(ScriptableObject.Instantiate(cardSet));
        }

        var baseCardDatas = new List<BaseCardData>();
        foreach (CardSetData cardSet in cardSetDataCopies) {
            baseCardDatas.AddRange(cardSet.cards);
        }

        return baseCardDatas;
    }

    private List<CardSetData> LoadCardSetData() {
        return InventoryController.Instance.GetCardSets();
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