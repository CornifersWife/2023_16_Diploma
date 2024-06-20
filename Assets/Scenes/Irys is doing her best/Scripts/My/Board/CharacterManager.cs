using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using Scenes.Irys_is_doing_her_best.Scripts.My.Cards;
using UnityEngine;
using UnityEngine.Events;

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

        private bool isYourTurn = false; //only true after all the "start of turn" things happen
        [ShowNativeProperty] private bool IsYourTurn => isYourTurn;


        private static CardEvent onCardPlayed = new CardEvent();


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

        // ReSharper disable Unity.PerformanceAnalysis
        public IEnumerator Draw(int amount) {
            if (amount <= 0) {
                Debug.LogError("Player just tried to draw 0 or less cards");
                yield return null;
            }

            if (!deck.Cards.Any()) {
                //TODO implement feedback
                deck.NoMoreCards();
                yield return null;
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

            yield return hand.DrawManyCoroutine(xd);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public IEnumerator StartOfTurn() {
            manaManager.RefreshMana();
            yield return Draw(1);
            Debug.Log("started");
            //TODO trigger all start of turn effects
        }

        public IEnumerator EndOfTurn() {
            //TODO trigger all start of turn effects
            yield return null;
        }
    }
}