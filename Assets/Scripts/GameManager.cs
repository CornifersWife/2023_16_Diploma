using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {
    public DeckManager playerDeck;
    public HandManager playerHand;
    
    public DeckManager enemyDeck;
    public HandManager enemyHand;

    public ManaManager playerMana;
    public ManaManager enemyMana;
    
    public Board board;

    public GameObject cardPrefab;

    public static GameManager Instance { get; private set; }
    
    
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }
    void Start() {
        foreach (var cardSpot in FindObjectsOfType<CardSpot>()) {
            SubscribeToCardSpot(cardSpot);
        }
    }

    public CardDisplay CreateCardInstance(BaseCardData cardData) {
        GameObject cardObj = Instantiate(cardPrefab, transform);
        CardDisplay newCardDisplay = cardObj.GetComponent<CardDisplay>();
        newCardDisplay.SetupCard(Instantiate(cardData));
        return newCardDisplay;
    }
    public CardDisplay CreateCardInstance(BaseCardData cardData, Transform newTransform) {
        GameObject cardObj = Instantiate(cardPrefab, transform);
        cardObj.transform.position = newTransform.position;
        CardDisplay newCardDisplay = cardObj.GetComponent<CardDisplay>();
        newCardDisplay.SetupCard(Instantiate(cardData));
        return newCardDisplay;
    }
    private void OnCardPlayed(CardSpot cardSpot, CardDisplay cardDisplay) {
        var mana = cardSpot.isPlayers ? playerMana : enemyMana;
        if (!mana.CanPlayCard(cardDisplay)) {
            cardDisplay.GetComponent<DragAndDrop>().SnapBack();
            return;
        }
        var hand = cardSpot.isPlayers ? playerHand : enemyHand;
        cardSpot.SetCardDisplay(cardDisplay);
        mana.UseMana(cardDisplay);
        hand.RemoveCardFromHand(cardDisplay);
        Transform cardTransform;
        (cardTransform = cardDisplay.transform).SetParent(cardSpot.transform);
        cardTransform.localPosition = Vector3.zero;
        cardTransform.position = cardSpot.transform.position;
        cardDisplay.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    private void SubscribeToCardSpot(CardSpot cardSpot) {
        cardSpot.Play += OnCardPlayed;
    }
    
    public void PlayerDrawCard() {
        BaseCardData drawnCard = playerDeck.DrawCard();
        if (drawnCard != null) {
            playerHand.AddCardToHand(drawnCard);
        }
    }
    
    public bool EnemyPlayMinion() {
        var availableCards=enemyHand.hand
            .Where(card => card.cardData is MinionCardData  && enemyMana.CanPlayCard(card)).ToArray();
        var availableBoardSpaces = board.enemyMinions
            .Where(space => space.IsEmpty()).ToArray();
        if (availableCards.Length <= 0 || availableBoardSpaces.Length <= 0) {
            return false;
        }
        var card = availableCards[Random.Range(0, availableCards.Count())];
        var boardSpot = availableBoardSpaces[Random.Range(0, availableBoardSpaces.Count())];
        boardSpot.CardDisplay = card;
        return true;
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
    
    /*public void TestShuffle() {
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
    }*/
}