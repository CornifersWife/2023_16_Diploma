using System;
using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Board {
    public class CardSpot : MonoBehaviour {
        public Cards.Card card;
        public bool isPlayers;

        private void Awake() {
            isPlayers = GetComponentInParent<BoardSide>().isPlayers;
        }
    }
}