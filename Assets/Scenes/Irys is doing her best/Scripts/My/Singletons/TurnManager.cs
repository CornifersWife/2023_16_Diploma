using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Board {
    
    public class TurnManager : MonoBehaviour {
        [BoxGroup("Wait Times"), SerializeField]
        private float startOfGameWait = 0.1f;

        [BoxGroup("Wait Times"), SerializeField]
        private float turnChangeBuffer = 0.8f;

        public static TurnManager Instance { get; private set; }

        [NonSerialized] public bool gameHasEnded = false;
        [SerializeField] public bool isPlayersTurn = false;
        private bool playerCanPlay;

        [Space, SerializeField] private CharacterManager player; // don't use them in code, use playing/waiting
        [SerializeField] private CharacterManager enemy;
        private EnemyAi enemyAi;

        private CharacterManager playing;
        private CharacterManager waiting;

        [BoxGroup("Start of game"), SerializeField] [Label("Cards Drawn")]
        private int cardsAtStartOfGame;


        private void Awake() {
            if (!Instance)
                Instance = this;
            else
                Destroy(gameObject);

            enemyAi = enemy.GetComponent<EnemyAi>();
            SubscribeToHeroes();
        }

        private void SubscribeToHeroes() {
            player.hero.death += EndGame;
            enemy.hero.death += EndGame;
        }


        public void EndGame(bool isPlayersHero) {
            if (gameHasEnded) {
                Debug.Log(
                    "Game tried to end after it was finished"); //not sure if this code could happen, but probably 
                return;
            }

            gameHasEnded = true;
            var component = GetComponent<EndGameManager>();
            component.EndGame(isPlayersHero);
        }


        private void Start() {
            StartCoroutine(StartGame());
        }

        private void UpdatePlayers() {
            playing = isPlayersTurn ? player : enemy;
            waiting = isPlayersTurn ? enemy : player;
        }

        private IEnumerator StartGame() {
            yield return WaitForGameToFullyLoad();
            yield return StartingDraw();

            //TODO allow for the enemy to start the game
            yield return player.StartOfTurn();
            isPlayersTurn = true;
            UpdatePlayers();
        }


        private IEnumerator WaitForGameToFullyLoad() {
            yield return new WaitForSeconds(startOfGameWait);
        }

        private IEnumerator StartingDraw() {
            var playingDraw = StartCoroutine(player.Draw(cardsAtStartOfGame));
            var waitingDraw = StartCoroutine(enemy.Draw(cardsAtStartOfGame));

            yield return playingDraw;
            yield return waitingDraw;
        }

        public void ChangeTurnButton() {
            isPlayersTurn = false;
            StartCoroutine(ChangeTurn());
        }

        [Button(enabledMode: EButtonEnableMode.Playmode)]
        private IEnumerator ChangeTurn() {
            if (gameHasEnded)
                yield break;

            yield return ChangeTurnActions();
            yield return new WaitForSeconds(turnChangeBuffer);

            isPlayersTurn = waiting.isPlayers;
            UpdatePlayers();
            if (playing == enemy) {
                yield return enemyAi.PlayTurn();
                yield return new WaitForSeconds(turnChangeBuffer);
                StartCoroutine(ChangeTurn());
            }
        }

        private IEnumerator ChangeTurnActions() {
            yield return playing.EndOfTurn();
            yield return TurnChangeAnimation();
            yield return waiting.StartOfTurn();
        }

        //TODO
        private IEnumerator TurnChangeAnimation() {
            yield return null;
        }


        //TODO REMOVE for testing only

        [Button(enabledMode: EButtonEnableMode.Playmode)]
        private void WinGameButton() {
            EndGame(false);
        }

        [Button(enabledMode: EButtonEnableMode.Playmode)]
        private void LoseGameButton() {
            EndGame(true);
        }
        //
    }
}