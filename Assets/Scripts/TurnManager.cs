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
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TurnSystem
{
    public interface IActor {
        bool IsAI { get; }
        bool IsAlive { get; }
        IEnumerator DoTurn();
    }

    public class CheckCondition : ScriptableObject
    {
        public virtual bool HasSatisfied() { return false; }
    }

    /// <summary>
    /// define 'Enemy' tag in Unity, set tag of dead enemies to 'Dead'
    /// </summary>
    [CreateAssetMenu(fileName = "win_condition", menuName = "Basic Win Condition", order = 0)]
    public class BasicWinCondition : CheckCondition
    {
        public override bool HasSatisfied() {
            return GameObject.FindGameObjectsWithTag("Enemy").Length == 0;
        }
    }

    /// <summary>
    /// set tag of dead Players to 'Dead'
    /// </summary>
    [CreateAssetMenu(fileName = "lose_condition", menuName = "Basic Lose Condition", order = 0)]
    public class BasicLoseCondition : CheckCondition
    {
        public override bool HasSatisfied()
        {
            return GameObject.FindGameObjectsWithTag("Player").Length == 0;
        }
    }

    public class TurnManager : MonoBehaviour
    {
        private bool _gameStarted = false;
        private List<IActor> _players = new List<IActor>();
        private List<IActor> _enemy = new List<IActor>();

        [Header("Events")]
        public UnityEvent OnGameStarted;
        public UnityEvent OnGameWon;
        public UnityEvent OnGameLost;

        [Header("Conditions")]
        public CheckCondition[] winConditions;
        public CheckCondition[] loseConditions;

        public void StartGame()
        {
            _gameStarted = true;

            if (OnGameStarted != null)
                OnGameStarted.Invoke();
        }

        public bool HasGameStarted
        {
            get
            {
                return _gameStarted;
            }
        }

        public bool HasWonGame
        {
            get
            {
                foreach (CheckCondition check in winConditions)
                {
                    if (!check.HasSatisfied()) return false;
                }
                return true;
            }
        }

        public bool HasLostGame
        {
            get
            {
                foreach (CheckCondition check in loseConditions)
                {
                    if (!check.HasSatisfied()) return false;
                }
                return true;
            }
        }

        public bool IsGameComplete
        {
            get
            {                
                return HasWonGame || HasLostGame;
            }
        }

        // Start is called before the first frame update
        IEnumerator Start()
        {
            yield return new WaitUntil(() => { return HasGameStarted; });

            foreach(GameObject actor in GameObject.FindGameObjectsWithTag("actors"))
            {
                IActor iactor = actor.GetComponent<IActor>();

                if (iactor.IsAI)
                    _enemy.Add(iactor);
                else
                    _players.Add(iactor);
            }

            while(!IsGameComplete)
            {
                foreach(IActor player in _players)
                {
                    yield return StartCoroutine(player.DoTurn());
                }

                foreach (IActor enemy in _enemy)
                {
                    yield return StartCoroutine(enemy.DoTurn());
                }
            }

            if(HasWonGame)
            {
                if (OnGameWon != null)
                    OnGameWon.Invoke();
            }

            if (HasLostGame)
            {
                if (OnGameLost != null)
                    OnGameLost.Invoke();
            }
        }
    }
}