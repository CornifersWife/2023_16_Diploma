using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Board {
    public class HandManager : MonoBehaviour {
        private List<Cards.Card> cards = new List<Cards.Card>();

        public List<Cards.Card> Cards {
            get {
                UpdateCardPositions();
                return cards;
            }
            set {
                cards = value;
                UpdateCardPositions();
            }
        }

        [Space] [Header("Distance Between Cards")] [SerializeField] [Range(2, 100)]
        public float distanceScalar = 10f;

        private void UpdateCardPositions() {
            float cardDistance = cards.Count > 1 ? DistanceBetweenCardsCalculator() : 0.01f;
            float totalWidth = cardDistance * cards.Count;
            var leftMostPosition = transform.position;
            leftMostPosition.x -= (totalWidth / 2f);
            for (int i = 0; i < cards.Count; i++) {
                var newPosition = leftMostPosition;
                newPosition.x += (i * cardDistance);
                cards[i].Move(newPosition);
            }
        }


        private float DistanceBetweenCardsCalculator() {
            float x = cards.Count - 1;
            float exponent = (x * -1f) / distanceScalar;
            float output = Mathf.Pow(10, exponent);
            return output;
        }
        
    }
}