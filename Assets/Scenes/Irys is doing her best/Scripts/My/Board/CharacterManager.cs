using System;
using System.Collections;
using System.Linq;
using Scenes.Irys_is_doing_her_best.Scripts.My.Cards;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Board {
    public class CharacterManager : MonoBehaviour {
        [Header("Data")] public HandManager hand;
        [SerializeField] public DeckManager deck;
        [SerializeField] public Hero hero;
        [SerializeField] public BoardSide boardSide;
        [SerializeField] public bool isPlayer;

        public UnityEvent<CardSpot> getCardSpots;

        [Space] [Header("Variables")] [SerializeField]
        private float timeBetweenDrawingManyCard = 1.1f;

        
        public void Draw() {
            if (!deck.Cards.Any()) {
                //TODO implement feedback
                Debug.LogError("No more cards honey");
                return;
            }

            var card = deck.Cards[0];
            card.GetComponent<CardDisplay>().ChangeCardVisible(isPlayer);
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