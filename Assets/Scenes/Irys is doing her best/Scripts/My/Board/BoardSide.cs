using System;
using System.Collections.Generic;
using System.Linq;
using Scenes.Irys_is_doing_her_best.Scripts.My.Interfaces;
using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Board {
    public class BoardSide : MonoBehaviour {
        [SerializeField] public CardSpot[] cardSpots = new CardSpot[4];
        private bool isPlayers { get; set; }

        private void Awake() {
            isPlayers = CompareTag("Player");
        }

        private void Start() {
            GetCardSpots();
        }

        private void GetCardSpots() {
            cardSpots = GetComponentsInChildren<CardSpot>();
        }

        public List<CardSpot> GetEmptyCardSpots() {
            return cardSpots.Where(e => e.card is null).ToList();
        }

        public List<Cards.Card> GetDamageableTargets() {
            var cards = cardSpots.Select(e => e.card);
            return cards.Where(e => e is IDamageable).ToList();
        }
        
        public List<Cards.Card> GetAttackers() {
            var cards = cardSpots.Select(e => e.card);
            return cards.Where(e => e is IAttacker).ToList();
        }
    }
}