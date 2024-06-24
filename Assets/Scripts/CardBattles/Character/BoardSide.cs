#nullable enable
using System.Collections.Generic;
using System.Linq;
using CardBattles.CardScripts;
using CardBattles.Interfaces;
using NaughtyAttributes;
using UnityEngine;

namespace CardBattles.Character {
    public class BoardSide : MonoBehaviour {
        [SerializeField] public CardSpot[] cardSpots = new CardSpot[4];
        [SerializeField,Required] public Hero.Hero hero = null!;
        private bool isPlayers;

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


        public List<IDamageable?> GetIDamageables() {
            List<IDamageable?> output = new List<IDamageable?>();
            var cards = cardSpots.Select(e => e.card).ToList();
            if (cards.Count != cardSpots.Length) {
                Debug.LogError(":(");
            }
            foreach (var card in cards) {
                if (card is not IDamageable damageable)
                    output.Add(hero);
                else {
                    output.Add(damageable);
                }
            }
            if (output.Count != cardSpots.Length) {
                Debug.LogError(">:(");
            }
            return output;
        }


        //TODO A BUG WHERE IF FIRST IS NULL IT DOESNT PICK IT UP
        public List<IAttacker?> GetIAttackers() {
            List<IAttacker?> output = new List<IAttacker?>();

            var cards = cardSpots.Select(e => e?.card).ToList();
            if (cards.Count != cardSpots.Length) {
                Debug.LogError(":(");
            }
            foreach (var card in cards) {
                if (card is not IAttacker attacker) {
                    output.Add(null);
                }
                else {
                    output.Add(attacker);
                }
            }

            return output;
        }
    }
}