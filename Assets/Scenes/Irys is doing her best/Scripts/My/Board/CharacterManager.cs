using System;
using System.Collections;
using System.Linq;
using Scenes.Irys_is_doing_her_best.Scripts.My.Cards;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Board {
    public class CharacterManager : MonoBehaviour {
        [Serializable]
        public class CardEvent : UnityEvent<Cards.Card, CardSpot> {
        }

        [Header("Data")] [SerializeField] public HandManager hand;
        [SerializeField] public DeckManager deck;
        [SerializeField] public Hero hero;
        [SerializeField] public BoardSide boardSide;
        [SerializeField] public ManaManager manaManager;
        [SerializeField] public bool isPlayers;

        private static CardEvent onCardPlayed = new CardEvent();

        [Space] [Header("Variables")] [SerializeField]
        private float timeBetweenDrawingManyCard = 1.1f;

        public void Awake() {
            onCardPlayed.AddListener(OnCardPlayedHandler);
        }

        public void OnDestroy() {
            onCardPlayed.RemoveListener(OnCardPlayedHandler);
        }

        public static void PlayACard(Cards.Card card, CardSpot cardSpot) {
            onCardPlayed.Invoke(card, cardSpot);
        }
        
        private void OnCardPlayedHandler(Cards.Card card, CardSpot cardSpot) {
            if (card.isPlayers != isPlayers)
                return;
            if (!manaManager.TryUseMana(card)) {
                return;
            }
            MoveCardToCardSpot(card,cardSpot);
            hand.UpdateCardPositions();
        }

        private void MoveCardToCardSpot(Cards.Card card, CardSpot cardSpot) {
            card.GetComponent<CardDisplay>().ChangeCardVisible(true);
            card.GetComponent<RectTransform>().position = cardSpot.GetComponent<RectTransform>().position;
            card.GetComponent<CardDragging>().droppedOnSlot = true;
            card.transform.SetParent(cardSpot.transform);
            cardSpot.cardObject = card.gameObject;
        }

        private void Draw() {
            if (!deck.Cards.Any()) {
                //TODO implement feedback
                Debug.LogError("No more cards honey");
                return;
            }

            var card = deck.Cards[0];
            card.GetComponent<CardDisplay>().ChangeCardVisible(isPlayers);
            card.GetComponent<CardDragging>().enabled = true;
            hand.DrawACard(card);
            deck.Cards.RemoveAt(0);
        }

        public void Draw(int amount) {
            StartCoroutine(DrawMany(amount));
        }

        private IEnumerator DrawMany(int amount) {
            for (int i = 0; i < amount; i++) {
                Draw();
                yield return new WaitForSeconds(timeBetweenDrawingManyCard);
            }

            yield return new WaitForSeconds(1f);
            hand.UpdateCardPositions();
        }

        
    }
}