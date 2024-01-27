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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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

    /// <summary>
    /// define 'Enemy' tag in Unity, set tag of dead enemies to 'Dead'
    /// </summary>
    [CreateAssetMenu(fileName = "win_condition", menuName = "Basic Win Condition", order = 0)]
    public class BasicWinCondition : CheckCondition {
        public override bool HasSatisfied() {
            return GameObject.FindGameObjectsWithTag("Enemy").Length == 0;
        }
    }

    /// <summary>
    /// set tag of dead Players to 'Dead'
    /// </summary>
    [CreateAssetMenu(fileName = "lose_condition", menuName = "Basic Lose Condition", order = 0)]
    public class BasicLoseCondition : CheckCondition {
        public override bool HasSatisfied() {
            return GameObject.FindGameObjectsWithTag("Player").Length == 0;
        }
    }

    public class GameEnd : TurnManager
    {
        //public 
    }

    public class TurnManager : MonoBehaviour {
        [SerializeField]public bool isPlayerTurn = true; // Flag to track whose turn it is
        [SerializeField] public float enemyDelay = 1f;
        
        private bool _gameStarted = false;
        
        // Reference to the HandManager and Board for the player
        public HandManager playerHand;
        public DeckManager playerDeck;

        // Reference to the HandManager and Board for the enemy (AI)
        public HandManager enemyHand;
        public DeckManager enemyDeck;
        public Board board;

        [Header("Events")] public UnityEvent OnGameStarted;
        public UnityEvent OnGameWon;
        public UnityEvent OnGameLost;

        [Header("Conditions")] public CheckCondition[] winConditions;
        public CheckCondition[] loseConditions;

        public void StartGame() {
            _gameStarted = true;

            if (OnGameStarted != null)
                OnGameStarted.Invoke(); //
        }
        [SerializeField]public bool HasGameStarted {
            //each player draws 5 cards
            get { return _gameStarted; }
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
        
        //Used to get first five cards
        public void startingHand() { 
            playerDeck.Shuffle();
            enemyDeck.Shuffle();
            for(int i = 0; i < 5; i++){
                playerHand.DrawACard();
                enemyHand.DrawACard();
            }
        }
        
        // Start is called before the first frame update
        IEnumerator Start() {
            startingHand();
            GameObject.FindWithTag("Player").GetComponent<ManaManager>().NextRound();
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

        private void StartOfTurn() {
            if (isPlayerTurn) StartPlayerTurn();
            else {
                StartEnemyTurn();
            }
        }

        private void StartPlayerTurn() {
            playerHand.DrawACard();
            //Refresh mana
            GameObject.FindWithTag("Player").GetComponent<ManaManager>().NextRound();
        }

        private void StartEnemyTurn() {
            enemyHand.DrawACard();
            //Refresh mana
            GameObject.FindWithTag("Enemy").GetComponent<ManaManager>().NextRound();
        }

        private void EndOfTurn(bool isPlayer) {
            // Perform actions that happen at the end of the turn
            // Example: Minions attack
            board.MinionsAttack(isPlayer);
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
            board.MinionsAttack(true);
        }

        private IEnumerator OpponentTurnRoutine() {
            //refreshmana
            //addmaxmana
            StartOfTurn();
            yield return new WaitForSeconds(enemyDelay);
            
            /* DOCELOWO
             * while( ( has playable card (manacost<= cardcost) ) && (has board space)) 
             *      choose random playable card
             *      play on random playable space
             *      yield return new WaitForSeconds(0.5f)
            */
            while (enemyHand.hand.Count > 0 && board.HasEmptySpace(false)) { //50% to stop after each card
                List<int> avalibleSpaces = new List<int>();
                for (int i = 0; i < board.opponentMinions.Length; i++) {
                    if(board.opponentMinions[i] is null)
                        avalibleSpaces.Add(i);
                }

                int cardIndex = Random.Range(0, enemyHand.hand.Count);
                int boardSpaceIndex = avalibleSpaces[Random.Range(0, avalibleSpaces.Count)];
                BaseCardData playedCard = enemyHand.hand[cardIndex];
                if (playedCard is MinionCardData) {
                    board.AddMinionToBoard((MinionCardData)playedCard, false, boardSpaceIndex);
                    enemyHand.RemoveCardFromHand(playedCard);
                }
                yield return new WaitForSeconds(enemyDelay);

                if (Random.Range(0, 2) > 0) break;
            }
            EndOfTurn(false);
            yield return new WaitForSeconds(enemyDelay);
            isPlayerTurn = true;
            StartOfTurn();
        }
    }
}