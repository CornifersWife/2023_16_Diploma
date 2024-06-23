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

        [ShowNativeProperty]
        public bool IsYourTurn {
            get {
                if (Application.isPlaying)
                    return TurnManager.Instance.isPlayersTurn == isPlayers;
                return false;
            }
        }

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
            if (!IsYourTurn)
                return;
            if (!manaManager.TryUseMana(card)) {
                return;
            }

            MoveCardToCardSpot(card, cardSpot);
            hand.UpdateCardPositions();
            card.GetComponent<CardDragging>().droppedOnSlot = true;
        }

        public IEnumerator PlayCardCoroutine(Cards.Card card, CardSpot cardSpot, float time) { //time defined in EnemyAi
            OnCardPlayedHandler(card, cardSpot);
            yield return new WaitForSeconds(time);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void MoveCardToCardSpot(Cards.Card card, CardSpot cardSpot) {
            //TODO change all of these to functions inside card 
            if (!isPlayers)
                StartCoroutine(
                    card.GetComponent<CardAnimation>()
                        .PlayAnimation(cardSpot));
            
            card.GetComponent<CardDisplay>().ChangeCardVisible(true);
            if (isPlayers)
                card.GetComponent<RectTransform>().position =
                    cardSpot.GetComponent<RectTransform>().position; //prevTODO aside from this
            else {
                StartCoroutine(
                    card.GetComponent<CardAnimation>()
                        .PlayAnimation(cardSpot));
            }

            card.GetComponent<CardDragging>().droppedOnSlot = true;
            card.transform.SetParent(cardSpot.transform);

            //TODO change so that it works not only from hand
            hand.Cards.Remove(card);
            cardSpot.card = card;
            card.AssignCardSpot(cardSpot);
        }

        
        // ReSharper disable Unity.PerformanceAnalysis
        public IEnumerator Draw(int amount, int cost = 0) {
            if (amount <= 0) {
                Debug.LogError("Player just tried to draw 0 or less cards");
                yield break;
            }

            if (cost > 0)
                if (!manaManager.TryUseMana(cost))
                    yield break;

            var cardsToDraw = new List<Cards.Card>();
            for (int i = 0; i < amount; i++) {
                if (!deck.Cards.Any()) {
                    
                    //TODO ADD FEEDBACK CURRENTLY IS JUST DEBUG LOG
                    deck.NoMoreCards();
                    break;
                }

                cardsToDraw.Add(deck.Cards[0]);
                deck.Cards.RemoveAt(0);
            }

            yield return hand.DrawManyCoroutine(cardsToDraw);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public IEnumerator StartOfTurn() {
            yield return Draw(1);
            manaManager.RefreshMana();
        }

        public void DrawACard() {
            StartCoroutine(Draw(1, 1));
        }
        public IEnumerator EndOfTurn() {
            yield return BoardManager.Instance.Attack(isPlayers);
            yield return null;
        }
    }
}