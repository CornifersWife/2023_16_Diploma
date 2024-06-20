using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Board {
    public class TurnManager : MonoBehaviour {
        [SerializeField] private float startOfGameWait = 0.1f;

        public static TurnManager Instance { get; private set; }

        private void Awake() {
            if (!Instance) {
                Instance = this;
            }
            else {
                Destroy(gameObject);
            }
        }

        [BoxGroup("Info")] private bool isPlayersTurn = true;

        [Space] [SerializeField] private CharacterManager player; // don't use them in code, use playing/waiting
        [SerializeField] private CharacterManager enemy;

        private CharacterManager playing;
        private CharacterManager waiting;

        [Space] [Header("Data")] [SerializeField]
        private int cardsAtStartOfGame;


        private void Start() {
            playing = isPlayersTurn ? player : enemy;
            waiting = isPlayersTurn ? enemy : player;

            StartCoroutine(StartGame());
        }

        private IEnumerator StartGame() {
            yield return WaitForGameToFullyLoad();

            StartingDraw();
        }

        private IEnumerator WaitForGameToFullyLoad() {
            yield return new WaitForSeconds(startOfGameWait);
        }

        private void StartingDraw() {
            playing.Draw(cardsAtStartOfGame);
            waiting.Draw(cardsAtStartOfGame);
        }

        public void ChangeTurn() {
            StartCoroutine("ChangeTurnCoroutine");
        }

        private IEnumerator ChangeTurnCoroutine() {
            yield return playing.EndOfTurn();
            yield return TurnChangeAnimation();
            yield return waiting.StartOfTurn();
            (playing, waiting) = (waiting, playing);
        }

        //TODO
        private IEnumerator TurnChangeAnimation() {
            yield return null;
        }
    }
}