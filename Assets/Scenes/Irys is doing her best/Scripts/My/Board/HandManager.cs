using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Board {
    public class HandManager : MonoBehaviour {
        [BoxGroup("Animations")] [SerializeField]
        private float timeBetweenDrawingManyCard = 1.1f;

        private List<Cards.Card> cards = new List<Cards.Card>();

        public List<Cards.Card> Cards {
            get => cards;
            private set => cards = value;
        }

        public bool isPlayers { get; set; }

        private void Awake() {
            isPlayers = CompareTag("Player");
        }


        [Space] [Header("Distance Between Cards")] [SerializeField] [Range(10, 1000)]
        public float distanceScalar = 10f;

        [SerializeField] [Range(100, 1000)] public float distanceMulti = 300f;


        /*
        public void DrawCards(List<Cards.Card> cardsToDraw) {
            StartCoroutine(DrawManyCoroutine(cardsToDraw));
        }*/

        //TODO move to another file?
        public IEnumerator DrawManyCoroutine(List<Cards.Card> cardsToDraw) {
            AddNewCards(cardsToDraw);

            var finalPositions = CalculateCardPositions(Cards.Count);
            var couroutines = new List<Coroutine>();


            int i = 0;
            for (; i < (Cards.Count - cardsToDraw.Count); i++) {
                StartCoroutine(Cards[i].Move(finalPositions[i]));
            }


            for (int j = 0; i < Cards.Count && j < cardsToDraw.Count; i++, j++) {
                Cards[i].IsDrawn();

                var huh = Cards[i].DrawAnimation(finalPositions[i]);


                couroutines.Add(StartCoroutine(huh));
                //    TrackCoroutine(coroutine,
               //         () => coroutineCompletionFlags[localIndex] = true));

                if (j < cardsToDraw.Count - 1) {
                    yield return new WaitForSeconds(timeBetweenDrawingManyCard);
                }
            }

            foreach (var coroutine in couroutines) {
                yield return coroutine;
            }
            //yield return new WaitUntil(() => coroutineCompletionFlags.All(flag => flag));

            //UpdateCardPositions();
        }


        private void AddNewCards(List<Cards.Card> cardsAdded) {
            cardsAdded.ForEach(card => {
                card.transform.SetParent(transform, true);
                Cards.Add(card);
            });
        }

        public void UpdateCardPositions(int additionalCards = 0) {
            var newHandPositions = CalculateCardPositions(cards.Count + additionalCards);
            for (int i = 0; i < cards.Count; i++) {
                StartCoroutine(cards[i].Move(newHandPositions[i]));
            }
        }

        private List<Vector3> CalculateCardPositions(int numberOfCards) {
            var output = new List<Vector3>();
            float cardDistance = numberOfCards > 1 ? DistanceBetweenCardsCalculator(numberOfCards) : 0.01f;
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
            float x = numberOfCards - 1;
            float exponent = (x * -1f) / distanceScalar;
            float output = Mathf.Pow(10, exponent);
            output *= distanceMulti;
            return output;
        }

        private IEnumerator TrackCoroutine(IEnumerator coroutine, Action onComplete) {
            yield return coroutine;
            onComplete();
        }
    }
}