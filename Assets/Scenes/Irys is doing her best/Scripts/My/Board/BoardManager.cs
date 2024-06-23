using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Board {
    public class BoardManager : MonoBehaviour {
        
        public static BoardManager Instance { get; private set; }

        [SerializeField] private BoardSide player;
        [SerializeField] private BoardSide enemy;
        
        
        
        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(gameObject);
            }
        }


        private void Attack(bool isPlayers) {
            var attacker = isPlayers ? player : enemy;
            var target = isPlayers ? enemy : player;

            var attackers = attacker.GetIAttackers();
            var targets = target.GetIDamageables();

            for (int i = 0; i < attackers.Count; i++) {
                attackers[i].AttackTarget(targets[i]);
            }
        }

        private void DetermineTargets() {
            
        }
    }
}