using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Board {
    public class TurnManager : MonoBehaviour {
        [BoxGroup("Wait Times"), SerializeField]
        private float startOfGameWait = 0.1f;

        [BoxGroup("Wait Times"), SerializeField]
        private float turnChangeBuffer = 0.8f;
        
        public static TurnManager Instance { get; private set; }

        // private bool gameHasStarted = false;
        [NonSerialized]
        public bool gameHasEnded = false;
        [SerializeField] public bool isPlayersTurn = false;

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
        }


        private void Start() {
            StartCoroutine(StartGame());
        }
        private void UpdatePlayers(){
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

        [Button(enabledMode: EButtonEnableMode.Playmode)]
        public IEnumerator ChangeTurn() {
            yield return ChangeTurnActions();
            yield return new WaitForSeconds(turnChangeBuffer);
            
            isPlayersTurn = !isPlayersTurn;
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
    }
}