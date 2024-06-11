using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Board {
    public class TurnManager : MonoBehaviour {
        public static TurnManager Instance { get; private set; }

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            else {
                Destroy(gameObject);
            }
        }

        [Header("TURN")] [SerializeField] private bool isPlayersTurn = true;
        [Space]
        [SerializeField] private CharacterManager player;
        [SerializeField] private CharacterManager enemy;

        [Space] [Header("Data")] [SerializeField]
        private int cardsAtStartOfGame;

        private void Start() {
            StartCoroutine(WaitForGameToFullyLoad());
        }

        private IEnumerator WaitForGameToFullyLoad() {
            yield return new WaitForSeconds(1f);
            StartingDraw();
        }
        private void StartingDraw() {
            player.Draw(cardsAtStartOfGame);
            enemy.Draw(cardsAtStartOfGame);
            
        }
    }
}