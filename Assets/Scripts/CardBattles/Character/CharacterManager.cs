using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CardBattles.CardScripts;
using CardBattles.CardScripts.Additional;
using CardBattles.Character.Mana;
using CardBattles.Enums;
using CardBattles.Interfaces.InterfaceObjects;
using CardBattles.Managers;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace CardBattles.Character {
    public class CharacterManager : PlayerEnemyMonoBehaviour {
        public interface ITarget {
            //CardSpot, Minion, Hero, Board
        }
        [Serializable]
        public class CardEvent : UnityEvent<Card, CardSpot> {
        }

        
        [Serializable]
        public class CardEventNew : UnityEvent<Card, ITarget> {
        }
        [Header("Data")] [SerializeField] public HandManager hand;
        [SerializeField] public DeckManager deck;
        [SerializeField] public Hero.Hero hero;
        [SerializeField] public BoardSide boardSide;
        [SerializeField] public ManaManager manaManager;

        [ShowNativeProperty]
        public bool IsYourTurn {
            get {
                if (Application.isPlaying)
                    return TurnManager.Instance.isPlayersTurn == IsPlayers;
                return false;
            }
        }
        
        //TODO remove
        private static CardEvent onCardPlayed = new CardEvent();
        private static CardEventNew onCardPlayedNew = new CardEventNew();


        public void Awake() {
            onCardPlayed.AddListener(OnCardPlayedHandler);
            onCardPlayedNew.AddListener(PlayCardHandler);
        }

        public void OnDestroy() {
            onCardPlayed.RemoveListener(OnCardPlayedHandler);
            onCardPlayedNew.AddListener(PlayCardHandler);

        }
        //TODO remove
        public static void PlayACard(Card card, CardSpot cardSpot) {
            onCardPlayed.Invoke(card, cardSpot);
        }

        public static void PlayACardNew(Card card, ITarget target) {
            onCardPlayedNew.Invoke(card, target);
            Debug.Log("added to reformat");
        }

        //new
        private void PlayCardHandler(Card card, ITarget target) {
            if (card.IsPlayers != IsPlayers)
                return;
            if (!IsYourTurn)
                return;
            if (!manaManager.TryUseMana(card)) {
                return;
            }
            CardManager.Instance.PlayACard(this, card, target);
        }
        
        //TODO TryToMoveToCardManager
        private void OnCardPlayedHandler(Card card, CardSpot cardSpot) {
            if (card.IsPlayers != IsPlayers)
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

        //TODO TryToMoveToCardManager
        public IEnumerator PlayCardCoroutine(Card card, CardSpot cardSpot, float time) { //time defined in EnemyAi
            OnCardPlayedHandler(card, cardSpot);
            yield return new WaitForSeconds(time);
        }
        
        //new
        public static void PlayACard(Card card, ITarget target) {
            onCardPlayedNew.Invoke(card,target);
        }
        
        //TODO TryToMoveToCardManager
        // ReSharper disable Unity.PerformanceAnalysis
        private void MoveCardToCardSpot(Card card, CardSpot cardSpot) {
            //TODO change all of these to functions inside card 
            if (!IsPlayers)
                StartCoroutine(
                    card.GetComponent<CardAnimation>()
                        .PlayToCardSpotAnimation(cardSpot));
            
            card.GetComponent<CardDisplay>().ChangeCardVisible(true);
            if (IsPlayers)
                card.GetComponent<RectTransform>().position =
                    cardSpot.GetComponent<RectTransform>().position; //prevTODO aside from this
            else {
                StartCoroutine(
                    card.GetComponent<CardAnimation>()
                        .PlayToCardSpotAnimation(cardSpot));
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

            var cardsToDraw = new List<Card>();
            for (int i = 0; i < amount; i++) {
                if (!deck.cards.Any()) {
                    
                    //TODO ADD FEEDBACK CURRENTLY IS JUST DEBUG LOG
                    deck.NoMoreCards();
                    break;
                }

                cardsToDraw.Add(deck.cards[0]);
                deck.cards.RemoveAt(0);
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
            yield return BoardManager.Instance.Attack(IsPlayers);
            foreach (var card in boardSide.GetNoNullCards()) {
                card.DoEffect(EffectTrigger.OnEndTurn);
            }
            yield return null;
        }
    }
}