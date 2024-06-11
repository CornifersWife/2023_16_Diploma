using System;
using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Board {
    public class BoardSide : MonoBehaviour {
        [SerializeField] public CardSpot[] cardSpots = new CardSpot[4];
        public bool isPlayer { get; set; }

        private void Awake() {
            if (CompareTag("Player"))
                isPlayer = true;
            else {
                isPlayer = false;
            }
        }

        private void Start() {
            GetCardSpots();
        }

        private void GetCardSpots() {
            cardSpots = GetComponentsInChildren<CardSpot>();
        }
    }
}
