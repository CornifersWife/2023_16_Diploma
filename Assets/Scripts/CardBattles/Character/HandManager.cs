using System.Collections;
using System.Collections.Generic;
using CardBattles.CardScripts;
using NaughtyAttributes;
using UnityEngine;
using Math = System.Math;

namespace CardBattles.Character {
    public class HandManager : MonoBehaviour {
        
        
        //TODO after adding each card, sort the objects in hand so the order matches the order in Cards
        //it will make all the cards lay on top of each other correctly
        
        [BoxGroup("Animations")] [SerializeField]
        private float timeBetweenDrawingManyCard = 1.1f;

        private List<Card> cards = new List<Card>();

        public List<Card> Cards {
            get => cards;
            set => cards = value;
        }

        private bool isPlayers;

        private void Awake() {
            isPlayers = CompareTag("Player");
        }

        [SerializeField] [Range(100, 1000)] public float distanceMulti = 300f;


        public void DestroyCard(Card card) {
            
        }
        public void RemoveCard(Card card) {
            if(!CardInHandCheck(card))
                return;
            Cards.Remove(card);
            UpdateCardPositions();
        }

        private bool CardInHandCheck(Card card) {
            var notInHand = !Cards.Contains(card);
            if(notInHand)
                Debug.LogError("Tried to remove or access a card that isnt contained in Cards");
            return !notInHand;
        }
        public IEnumerator DrawManyCoroutine(List<Card> cardsToDraw) {
            AddNewCards(cardsToDraw);

            var finalPositions = CalculateCardPositions(Cards.Count);
            var coroutines = new List<Coroutine>();


            int i = 0;
            for (; i < (Cards.Count - cardsToDraw.Count); i++) {
                StartCoroutine(Cards[i].Move(finalPositions[i]));
            }

            yield return new WaitForEndOfFrame();

            for (int j = 0; i < Cards.Count && j < cardsToDraw.Count; i++, j++) {
                Cards[i].ChangeCardVisible(isPlayers);
                Cards[i].transform.SetParent(transform, true);
                var coroutine = StartCoroutine(Cards[i].DrawAnimation(finalPositions[i]));

                coroutines.Add(coroutine);

                if (cardsToDraw.Count > 1) {
                    yield return new WaitForSeconds(timeBetweenDrawingManyCard);
                }
            }

            foreach (var coroutine in coroutines) {
                yield return coroutine;
            }

            foreach (var card in Cards) {
                card.IsDrawn();
            }
        }


        private void AddNewCards(List<Card> cardsAdded) {
            foreach (var card in cardsAdded) {
                Cards.Add(card);
            }
        }


        public void UpdateCardPositions(int additionalCards = 0) {
            var newHandPositions = CalculateCardPositions(Cards.Count + additionalCards);
            for (int i = 0; i < Cards.Count; i++) {
                StartCoroutine(Cards[i].Move(newHandPositions[i]));
            }
        }

        private List<Vector3> CalculateCardPositions(int numberOfCards) {
            var output = new List<Vector3>();
            float cardDistance = numberOfCards > 1 ? DistanceBetweenCardsCalculator(numberOfCards) : 0.01f;
            if (numberOfCards == 2)
                cardDistance /= 2;
            
            float totalWidth = cardDistance * numberOfCards;
            var leftMostPosition = transform.position;
            leftMostPosition.x -= (totalWidth / 2f);
            for (int i = 0; i < numberOfCards; i++) {
                var newPosition = leftMostPosition;
                newPosition.x += (i * cardDistance);
                output.Add(newPosition);
            }
            

            return output;
        }

        private float DistanceBetweenCardsCalculator(int numberOfCards) {
            double distance = Math.Log(numberOfCards + 1) * distanceMulti;
            distance /= (numberOfCards - 1);
            return (float)distance;
        }
    }
}