using System;
using System.Collections;
using System.Collections.Generic;
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


        public void Awake() {
            onCardPlayed.AddListener(OnCardPlayedHandler);
        }

        public void OnDestroy() {
            onCardPlayed.RemoveListener(OnCardPlayedHandler);
        }

        public static void PlayACard(Cards.Card card, CardSpot cardSpot) {
            Debug.Log("Yipee");
            onCardPlayed.Invoke(card, cardSpot);
        }

        private void OnCardPlayedHandler(Cards.Card card, CardSpot cardSpot) {
            if (card.isPlayers != isPlayers)
                return;
            if (!manaManager.TryUseMana(card)) {
                return;
            }

            MoveCardToCardSpot(card, cardSpot);
            hand.UpdateCardPositions();
            card.GetComponent<CardDragging>().droppedOnSlot = true;
        }

        private void MoveCardToCardSpot(Cards.Card card, CardSpot cardSpot) {
            card.GetComponent<CardDisplay>().ChangeCardVisible(true);
            card.GetComponent<RectTransform>().position = cardSpot.GetComponent<RectTransform>().position;
            card.GetComponent<CardDragging>().droppedOnSlot = true;
            card.transform.SetParent(cardSpot.transform);
            //TODO change so that it works not only from hand
            hand.Cards.Remove(card);
            cardSpot.card = card;
        }

        public void Draw(int amount) {
            if (amount <= 0) {
                Debug.LogError("Player just tried to draw 0 or less cards");
                return;
            }

            if (!deck.Cards.Any()) {
                //TODO implement feedback
                deck.NoMoreCards();
                return;
            }

            var xd = new List<Cards.Card>();
            for (int i = 0; i < amount; i++) {
                if (!deck.Cards.Any()) {
                    //TODO ADD FEEDBACK CURRENTLY IS JUST DEBUG LOG
                    deck.NoMoreCards();
                    break;
                }

                xd.Add(deck.Cards[0]);
                deck.Cards.RemoveAt(0);
            }

            hand.DrawCards(xd);
        }
    }
}