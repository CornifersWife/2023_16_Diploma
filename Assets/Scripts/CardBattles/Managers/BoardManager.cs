using System.Collections;
using System.Collections.Generic;
using CardBattles.CardScripts;
using CardBattles.Character;
using CardBattles.Enums;
using CardBattles.Interfaces;
using UnityEngine;

namespace CardBattles.Managers {
    public class BoardManager : MonoBehaviour {
        [SerializeField] private float betweenAttacksTime;

        public static BoardManager Instance { get; private set; }

        [SerializeField] private CharacterManager playerCharacter;
        [SerializeField] private CharacterManager enemyCharacter;
        private BoardSide player;
        private BoardSide enemy;

        private CharacterManager PlayingCharacter(bool isPlayers) => isPlayers ? playerCharacter : enemyCharacter;
        private CharacterManager WaitingCharacter(bool isPlayers) => isPlayers ? enemyCharacter : playerCharacter;

        private BoardSide Playing(bool isPlayers) => isPlayers ? player : enemy;
        private BoardSide Waiting(bool isPlayers) => isPlayers ? enemy : player;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            else {
                Destroy(gameObject);
            }

            player = playerCharacter.boardSide;
            enemy = enemyCharacter.boardSide;
        }


        public IEnumerator Attack(bool isPlayers) {
            var attacker = Playing(isPlayers);
            var target = Waiting(isPlayers);

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

        public delegate List<GameObject> TargetsDelegate(TargetType targetType, Card card);

        public List<GameObject> GetTargets(TargetType targetType, Card card) {
            bool isPlayers = card.isPlayers;
            List<GameObject> targets = new List<GameObject>();

            switch (targetType) {
                case TargetType.AllMinions:
                    targets.AddRange(player.GetNoNullCardsObjects());
                    targets.AddRange(enemy.GetNoNullCardsObjects());
                    break;
                case TargetType.EnemyMinions:
                    targets.AddRange(Waiting(isPlayers).GetNoNullCardsObjects());
                    break;
                case TargetType.YourMinions:
                    targets.AddRange(Playing(isPlayers).GetNoNullCardsObjects());
                    break;
                case TargetType.OpposingMinion:
                    targets.AddRange(GetOpposingCard(card));
                    break;
                case TargetType.AdjacentMinions:
                    targets.AddRange(Playing(isPlayers).GetAdjecentCards());
                    break;
                case TargetType.BothHeroes:
                    targets.Add(player.hero.gameObject);
                    targets.Add(enemy.hero.gameObject);
                    break;
                case TargetType.YourHero:
                    targets.Add(Playing(isPlayers).hero.gameObject);
                    break;
                case TargetType.OpposingHero:
                    targets.Add(Waiting(isPlayers).hero.gameObject);

                    break;
                case TargetType.CardSet:
                    targets.AddRange(PlayingCharacter(isPlayers).deck.GetCardFromSameCardSet(card));
                    break;
            }

            return targets;
        }

        private IEnumerable<GameObject> GetOpposingCard(Card card) {
            throw new System.NotImplementedException();
        }
    }
}