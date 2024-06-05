using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Board {
    public class BoardManager : MonoBehaviour {
        
        public static BoardManager Instance { get; private set; }

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(gameObject);
            }
        }

        private CardSpot[][] cardSpots;
    }
}