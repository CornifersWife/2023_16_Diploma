using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour {
    public List<CardDisplay> hand = new List<CardDisplay>();
    [SerializeField] private DeckManager deck;
    public bool isPlayers = true;
    [SerializeField] private float baseCardSpacing = 1.0f; 
    [SerializeField] private float animationDuration = 0.1f;

    [Header("card spacing")] 
    [SerializeField] private int cardAmountLimit = 4;

    [SerializeField] private float verticalCardSpacing = 0.05f;
    private void UpdateCardPositionsInHand() {
        var cardSpacing = baseCardSpacing;
        if (hand.Count > cardAmountLimit)
            cardSpacing = 3f / (hand.Count - (cardAmountLimit - 3)) ;
        float totalWidth = (hand.Count - 1) * cardSpacing;
        Vector3 startPos = -Vector3.left * totalWidth / 2;
        for (int i = 0; i < hand.Count; i++) {
            Vector3 targetPos = startPos + Vector3.left * (i * cardSpacing);
            targetPos += Vector3.up * (i * verticalCardSpacing);
            StartCoroutine(AnimateCardToPosition(hand[i].transform, targetPos, animationDuration));
        }
    }

    private IEnumerator AnimateCardToPosition(Transform cardTransform, Vector3 targetPosition, float duration) {
        Vector3 startPosition = cardTransform.localPosition;
        float time = 0;
        while (time < duration) {
            cardTransform.localPosition = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        cardTransform.localPosition = targetPosition;
    }

    public void DrawACard() {
        BaseCardData drawn = deck.DrawCard();
        if (drawn is null) {
            Debug.Log("Deck is empty");
            return;
        }
        AddCardToHand(drawn);
    }

    
    public void AddCardToHand(BaseCardData cardData) {
        CardDisplay newCardDisplay = GameManager.Instance.CreateCardInstance(cardData,deck.transform);
        if (!isPlayers) {
            newCardDisplay.GetComponent<DragAndDrop>().enabled = false;
        }
        newCardDisplay.transform.SetParent(transform);
        hand.Add(newCardDisplay);
        UpdateCardPositionsInHand();
    }

    public void RemoveCardFromHand(CardDisplay card) {
        if (hand.Remove(card))
            UpdateCardPositionsInHand();
    }
}