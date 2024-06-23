using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Scenes.Irys_is_doing_her_best.Scripts.My.Interfaces;
using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Board {
    public class BoardSide : MonoBehaviour {
        [SerializeField] public CardSpot[] cardSpots = new CardSpot[4];
        [SerializeField] public Hero hero;
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

        /*public List<Cards.Card> GetDamageableTargets() {
            var cards = cardSpots.Select(e => e.card);
            return cards.Where(e => e is IDamageable).ToList();
        }*/

        public List<IDamageable> GetIDamageables() {
            List<IDamageable> output = new List<IDamageable>();
            var cards = cardSpots.Select(e => e.card).ToList();
            foreach (var card in cards) {
                if (card is not IDamageable damageable)
                    output.Add(hero);
                else {
                    output.Add(damageable);
                }
            }

            return output;
        }


        public List<IAttacker> GetIAttackers() {
            List<IAttacker> output = new List<IAttacker>();
            
            var cards = cardSpots.Select(e => e.card);
            
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