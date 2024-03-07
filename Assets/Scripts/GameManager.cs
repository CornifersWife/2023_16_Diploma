using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public DeckManager playerDeck;
    public HandManager playerHand;
    
    public DeckManager enemyDeck;
    public HandManager enemyHand;
    
    public Board board;
    public int cardtoplay = -1;
    public int boardspacechosen = -1;

    public ButtonManager buttonManager;

    private Dictionary<MinionCardData, CardDisplay> minionToDisplayMap = new Dictionary<MinionCardData, CardDisplay>();

    public static GameManager Instance { get; private set; }
    
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this.gameObject);
        } else {
            Instance = this;
        }
    }
    
    public void RegisterMinionDisplay(MinionCardData minion, CardDisplay display) {
        if (!minionToDisplayMap.ContainsKey(minion)) {
            minionToDisplayMap.Add(minion, display);
        }
    }

    public void UnregisterMinionDisplay(MinionCardData minion) {
        if (minionToDisplayMap.ContainsKey(minion)) {
            minionToDisplayMap.Remove(minion);
        }
    }

    public CardDisplay GetCardDisplayForMinion(MinionCardData minion) {
        if (minionToDisplayMap.TryGetValue(minion, out CardDisplay display)) {
            return display;
        }
        return null;
    }
    
    // Call this method to test drawing a card
    public void PlayerPlayCard() {
        int handIndex = buttonManager.GetCardIndex(); // Get selected card index in hand
        if (playerHand.hand.Count > handIndex)
        {
            BaseCardData playedCard = playerHand.hand[handIndex];
            if (GameObject.FindWithTag("Player").GetComponent<ManaManager>().UseMana(playedCard)) {//check if player has enough mana
                if (playedCard is MinionCardData) {
                    bool success = board.AddMinionToBoard((MinionCardData)playedCard, buttonManager.GetSpotIndex(), buttonManager.GetToggle().transform);
                    if(success)
                        playerHand.RemoveCardFromHand(playedCard);
                }
                // Additional logic for other types of cards (like spells)
            }
        }
    }
    public void PlayerDrawCard() {
        BaseCardData drawnCard = playerDeck.DrawCard();
        if (drawnCard != null) {
            playerHand.AddCardToHand(drawnCard);
        }
    }
    
    public void EnemyPlayCard() {
        int handIndex = 0; // For testing, we'll play the first card in hand
        if (enemyHand.hand.Count > handIndex) {
            BaseCardData playedCard = enemyHand.hand[handIndex];
            if (GameObject.FindWithTag("Enemy").GetComponent<ManaManager>().UseMana(playedCard)) { //check if enemy has enough mana, but he don't use this function
                if (playedCard is MinionCardData) {
                    board.AddMinionToBoard((MinionCardData)playedCard, false);
                    enemyHand.RemoveCardFromHand(playedCard);
                }
                // Additional logic for other types of cards (like spells)
            }
        }
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
    
    //TODO in the future i want this to be how attacks are handled, it will become a lot easier to do anything with this
    //and in general i want more stuff to be in here instead of in diffrent places in code
    public void HandleAttack(MinionCardData attacker, object target) {
        CardDisplay attackerDisplay = GetCardDisplayForMinion(attacker);
        Vector3 targetPosition;

        if (target is MinionCardData targetMinion) {
            CardDisplay targetDisplay = GetCardDisplayForMinion(targetMinion);
            targetPosition = targetDisplay.transform.position;
            targetMinion.TakeDamage(attacker.power);
        } else if (target is Hero targetHero) {
            targetPosition = targetHero.transform.position;
            targetHero.TakeDamage(attacker.power);
        } else {
            Debug.LogError("Unknown target type for attack.");
            return;
        }

        attackerDisplay.AttackTarget(targetPosition);
        
    }

    // Call this method to test shuffling the deck
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