using System.Collections;
using System.Collections.Generic;
using CardBattles.Character;
using CardBattles.Interfaces;
using UnityEngine;

namespace CardBattles.Managers {
    public class BoardManager : MonoBehaviour {
        [SerializeField] private float betweenAttacksTime;

        public static BoardManager Instance { get; private set; }

        [SerializeField] private BoardSide player;
        [SerializeField] private BoardSide enemy;


        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            else {
                Destroy(gameObject);
            }
        }


        public IEnumerator Attack(bool isPlayers) {
            var attacker = isPlayers ? player : enemy;
            var target = isPlayers ? enemy : player;

            var coroutine = StartCoroutine(
                AttackCourutine(
                    attacker.GetIAttackers(),
                    target.GetIDamageables()));
            yield return coroutine;
        }

        private IEnumerator AttackCourutine(List<IAttacker> attackers, List<IDamageable> targets) {
            if (attackers.Count != 4)
                Debug.LogError($"{attackers.Count}  attack >:(");
            if (targets.Count != 4)
                Debug.LogError($"{targets.Count}  target >:(");


            for (int i = 0; i < 4; i++) {
                if (attackers[i] is null)
                    continue;
                if (attackers[i].GetAttack() <= 0)
                    continue;
                attackers[i].AttackTarget(targets[i]);
                yield return new WaitForSeconds(betweenAttacksTime);
            }
        }
    }
}