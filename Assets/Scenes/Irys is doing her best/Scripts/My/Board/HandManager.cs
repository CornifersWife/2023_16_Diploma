using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using Scenes.Irys_is_doing_her_best.Scripts.My.Cards;
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


        //TODO simplify Drawing so Draw and DrawMany dont have repeated code;
        public void DrawCard(Cards.Card card) {
            DrawCards(new List<Cards.Card> { card });
        }

        public void DrawCards(List<Cards.Card> cardsToDraw) {
            StartCoroutine(DrawManyCoroutine(cardsToDraw));
        }

        //TODO move to another file?
        public IEnumerator DrawManyCoroutine(List<Cards.Card> cardsToDraw) {
            //TODO Change it to updateCardPosition(cardsToDraw.count)
            var finalPositions = CalculateCardPositions(cardsToDraw.Count);
            int i = 0;
            for (; i < Cards.Count; i++) {
                Cards[i].Move(finalPositions[i]);
            }

            //
            yield return new WaitForFixedUpdate();
            foreach (var card in cardsToDraw) {
                //This is the only logic here, rest is animation
                card.transform.SetParent(transform, true);
                Cards.Add(card);
                //
            }

            for (; i < Cards.Count; i++) {
                Cards[i].IsDrawn();
                Cards[i].cardAnimation.DrawAnimation(finalPositions[i]);
                yield return new WaitForSeconds(timeBetweenDrawingManyCard);

            }
            
            UpdateCardPositions();
        }

        public void UpdateCardPositions(int additionalCards = 0) {
            var newHandPositions = CalculateCardPositions(cards.Count + additionalCards);
            for (int i = 0; i < cards.Count; i++) {
                cards[i].Move(newHandPositions[i]);
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
    }
}