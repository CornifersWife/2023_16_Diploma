using System;
using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Board {
    public class BoardSide : MonoBehaviour {
        [SerializeField] public CardSpot[] cardSpots = new CardSpot[4];
        public bool isPlayers { get; set; }

        private void Awake() {
            isPlayers = CompareTag("Player");

        }

        private void Start() {
            GetCardSpots();
        }

        private void GetCardSpots() {
            cardSpots = GetComponentsInChildren<CardSpot>();
        }
    }
}
