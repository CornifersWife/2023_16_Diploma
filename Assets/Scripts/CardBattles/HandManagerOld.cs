using System.Collections;
using System.Collections.Generic;
using Card;
using ScriptableObjects;
using UnityEngine;

public class HandManagerOld : MonoBehaviour {
    public List<CardOld> hand = new List<CardOld>();
    [SerializeField] private DeckManagerOld deck;
    public bool isPlayers = true;
    [SerializeField] private float baseCardSpacing = 1.0f; 
    [SerializeField] private float animationDuration = 0.1f;

    [Header("cardOld spacing")] 
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
            Debug.Log("DeckManager is empty");
            return;
        }
        AddCardToHand(drawn);
    }

    
    public void AddCardToHand(BaseCardData cardData) {
        CardOld newCardOld = GameManager.Instance.CreateCardInstance(cardData,deck.transform);
        if (!isPlayers) {
            newCardOld.GetComponent<DragAndDrop>().enabled = false;
        }
        newCardOld.transform.SetParent(transform);
        hand.Add(newCardOld);
        UpdateCardPositionsInHand();
    }

    public void RemoveCardFromHand(CardOld cardOld) {
        if (hand.Remove(cardOld))
            UpdateCardPositionsInHand();
    }
}