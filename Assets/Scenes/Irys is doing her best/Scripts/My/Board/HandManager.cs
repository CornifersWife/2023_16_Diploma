using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Scenes.Irys_is_doing_her_best.Scripts.My.Cards;
using Unity.VisualScripting;
using UnityEngine;
namespace Scenes.Irys_is_doing_her_best.Scripts.My.Board {
    public class HandManager : MonoBehaviour {
        
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
        
        
        public void DrawACard(Cards.Card card) {
            card.transform.SetParent(this.transform);
            var xd = CalculateCardPositions(cards.Count + 1)[cards.Count];
            card.GetComponent<CardAnimation>().DrawAnimation(xd);
            Cards.Add(card);
            UpdateCardPositions();
        }
        
        
        public void UpdateCardPositions() {
            var newHandPositions = CalculateCardPositions(cards.Count);
            for (int i = 0; i < cards.Count; i++) {
                cards[i].Move(newHandPositions[i]);

            }
        }

        private List<Vector3> CalculateCardPositions(int numberOfCard) {
            var output = new List<Vector3>();
            float cardDistance = numberOfCard > 1 ? DistanceBetweenCardsCalculator() : 0.01f;
            float totalWidth = cardDistance * numberOfCard;
            var leftMostPosition = transform.position;
            leftMostPosition.x -= (totalWidth / 2f);
            for (int i = 0; i < numberOfCard; i++) {
                var newPosition = leftMostPosition;
                newPosition.x += (i * cardDistance);
                output.Add(newPosition);
            }

            return output;
        }
        
        private float DistanceBetweenCardsCalculator() {
            float x = cards.Count - 1;
            float exponent = (x * -1f) / distanceScalar;
            float output = Mathf.Pow(10, exponent);
            output *= distanceMulti;
            return output;
        }
        
    }
}