using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {
    public DeckManager playerDeck;
    public HandManager playerHand;
    
    public DeckManager enemyDeck;
    public HandManager enemyHand;

    public ManaManager playerMana;
    public ManaManager enemyMana;
    
    public Board board;

    public static GameManager Instance { get; private set; }
    
    
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this.gameObject);
        } else {
            Instance = this;
        }
        
    }
    void Start() {
        playerMana = GameObject.FindWithTag("Player").GetComponent<ManaManager>();
        enemyMana = GameObject.FindWithTag("Enemy").GetComponent<ManaManager>();
        foreach (var cardSpot in FindObjectsOfType<CardSpot>()) {
            SubscribeToCardSpot(cardSpot);
        }
    }
    
    public void OnCardPlayed(CardSpot cardSpot, CardDisplay cardDisplay) {
        cardSpot.cardDisplay = cardDisplay;
        var hand = cardSpot.isPlayers ? playerHand : enemyHand;
        hand.RemoveCardFromHand(cardDisplay);
        cardDisplay.transform.SetParent(cardSpot.transform);
        cardDisplay.transform.localPosition = Vector3.zero;
        cardDisplay.transform.position = cardSpot.transform.position;
    }
    public void SubscribeToCardSpot(CardSpot cardSpot) {
        cardSpot.Play += OnCardPlayed;
    }
    public void UnsubscribeFromCardSpot(CardSpot cardSpot) {
        cardSpot.Play -= OnCardPlayed;
    }
    
    public void PlayerDrawCard() {
        BaseCardData drawnCard = playerDeck.DrawCard();
        if (drawnCard != null) {
            playerHand.AddCardToHand(drawnCard);
        }
    }
    
    public void EnemyPlayMinion() {
        //TODO check how manamanager works
        var avalibleCards=enemyHand.hand
            .Where(card => card.cardData is MinionCardData  && card.cardData.cost<=enemyMana.actualMana).ToArray();
        var avalibleBoardSpaces = board.enemyMinions
            .Where(space => space.IsEmpty()).ToArray();
        if (avalibleCards.Count() <= 0 || avalibleBoardSpaces.Count() <= 0) {
            return;
        }
        var card = avalibleCards[Random.Range(0, avalibleCards.Count())];
        var boardspot = avalibleBoardSpaces[Random.Range(0, avalibleCards.Count())];
        boardspot.CardDisplay = card;
    }
    
    public void EnemyDrawCard() {
        BaseCardData drawnCard = enemyDeck.DrawCard();
        if (drawnCard != null) {
            enemyHand.AddCardToHand(drawnCard);
        }
    }
    
    public void PlayerAttack() {
        board.MinionsAttack(true);
    }
    
    public void EnemyAttack() {
        board.MinionsAttack(false);
    }
    
    public void TestShuffle() {
        string ids = "Before shuffle: [";
        foreach (BaseCardData card in playerDeck.deck) {
            ids += card.id + ", ";
        }
        ids += "]";
        Debug.Log(ids);
        playerDeck.Shuffle();
        ids = "After shuffle: [";
        foreach (BaseCardData card in playerDeck.deck) {
            ids += card.id + ", ";
        }
        ids += "]";
        Debug.Log(ids);
    }
}