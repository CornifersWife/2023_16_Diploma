//MIT License

//Copyright (c) 2019 William Herrera

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace TurnSystem {
    public interface IActor {
        bool IsAI { get; }
        bool IsAlive { get; }
        IEnumerator DoTurn();
    }

    public class CheckCondition : ScriptableObject {
        public virtual bool HasSatisfied() {
            return false;
        }
    }

    [CreateAssetMenu(fileName = "win_condition", menuName = "Basic Win Condition", order = 0)]
    public class BasicWinCondition : CheckCondition {
        public override bool HasSatisfied() {
            return GameObject.FindGameObjectsWithTag("Enemy").Length == 0;
        }
    }

    [CreateAssetMenu(fileName = "lose_condition", menuName = "Basic Lose Condition", order = 0)]
    public class BasicLoseCondition : CheckCondition {
        public override bool HasSatisfied() {
            return GameObject.FindGameObjectsWithTag("Player").Length == 0;
        }
    }

    public class TurnManager : MonoBehaviour {
        [SerializeField] public bool isPlayerTurn = true; // Flag to track whose turn it is
        [SerializeField] public float enemyDelay = 1f;

        private bool gameStarted = false;

        public HandManager playerHand;
        public DeckManagerOld playerDeck;

        public HandManager enemyHand;
        public DeckManagerOld enemyDeck;

        [FormerlySerializedAs("playerMana")] public ActionPointManager playerActionPoint;
        [FormerlySerializedAs("enemyMana")] public ActionPointManager enemyActionPoint;

        [FormerlySerializedAs("board")] public BoardOld boardOld;

        [Header("Events")] public UnityEvent OnGameStarted;
        public UnityEvent OnGameWon;
        public UnityEvent OnGameLost;

        [Header("Conditions")] public CheckCondition[] winConditions;
        public CheckCondition[] loseConditions;


        public static TurnManager Instance { get; private set; }

        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
            }
            else {
                Instance = this;
            }
        }

        public void StartGame() {
            gameStarted = true;

            if (OnGameStarted != null)
                OnGameStarted.Invoke(); //
        }

        [SerializeField]
        public bool HasGameStarted {
            //each player draws 5 cards
            get { return gameStarted; }
        }

        public bool HasWonGame {
            get {
                foreach (CheckCondition check in winConditions) {
                    if (!check.HasSatisfied()) return false;
                }

                return true;
            }
        }

        public bool HasLostGame {
            get {
                foreach (CheckCondition check in loseConditions) {
                    if (!check.HasSatisfied()) return false;
                }

                return true;
            }
        }

        public bool IsGameComplete {
            get { return HasWonGame || HasLostGame; }
        }

        public void startingHand() {
            playerDeck.Shuffle();
            enemyDeck.Shuffle();
            for (int i = 0; i < 5; i++) {
                playerHand.DrawACard();
                enemyHand.DrawACard();
            }
        }

        IEnumerator Start() {
            startingHand();
            StartOfTurn();
            yield return new WaitUntil(() => { return HasGameStarted; });

            if (HasWonGame) {
                if (OnGameWon != null)
                    OnGameWon.Invoke();
            }

            if (HasLostGame) {
                if (OnGameLost != null)
                    OnGameLost.Invoke();
            }
        }

        public void gameEnd() {
            if (IsGameComplete) { //Is always giving true.
                if (HasWonGame) {
                    //Show something
                }

                if (HasLostGame) {
                    //Show something
                }

            }
        }

        private void StartOfTurn() {
            var actionPointManager = isPlayerTurn ? playerActionPoint : enemyActionPoint;
            var hand = isPlayerTurn ? playerHand : enemyHand;
            actionPointManager.StartRound();
            hand.DrawACard();
            if (isPlayerTurn) StartPlayerTurn();
            else {
                StartEnemyTurn();
            }

            gameEnd();
        }

        private void StartPlayerTurn() {
            //unique to player like do some animation
        }

        private void StartEnemyTurn() {
            //unique to enemy like do some animation
        }

        private void EndOfTurn(bool isPlayer) {
            boardOld.MinionsAttack(isPlayer);
        }

        public void EndPlayerTurn() {
            if (!isPlayerTurn) return; // Only proceed if it's currently the player's turn

            // Perform any end-of-turn actions for the player
            EndOfPlayerTurnActions();

            // Switch to the opponent's turn
            isPlayerTurn = false;
            StartCoroutine(OpponentTurnRoutine());
        }

        private void EndOfPlayerTurnActions() {
            boardOld.MinionsAttack(true);
        }

        private IEnumerator OpponentTurnRoutine() {
            StartOfTurn();
            yield return new WaitForSeconds(enemyDelay);


            while (true) {
                GameManager.Instance.EnemyPlayMinion();
                yield return new WaitForSeconds(enemyDelay);

                if (!GameManager.Instance.EnemyPlayMinion() || Random.Range(0, 2) > 0) break;
            }

            EndOfTurn(false);
            yield return new WaitForSeconds(enemyDelay);
            isPlayerTurn = true;
            StartOfTurn();
        }
    }
}