using System.Collections;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Board {
    public class EndGameManager : MonoBehaviour {

        [SerializeField,Required]
        private CanvasGroup rayCastBlocker;
        
        public void EndGame(bool isPlayersHero) {
            StartCoroutine(EndGameCoroutine(isPlayersHero));
            StartCoroutine(GameSlowDown());
        }

        
        private IEnumerator EndGameCoroutine(bool isPlayersHero) {
            rayCastBlocker.blocksRaycasts = true;
            if (isPlayersHero) {
                yield return LoseGame();
            }
            else {
                yield return WinGame();
            }
        }

        private IEnumerator WinGame() {
            Debug.Log("Congrats you won");
            yield return null;
        }

        private IEnumerator LoseGame() {
            Debug.Log("Boohoo :(");
            yield return null;
        }
        
        
        [BoxGroup("SlowDown"), SerializeField] private float endGameSlowDownFinalTimeScaleValue = 0.1f;
        [BoxGroup("SlowDown"), SerializeField] private float endGameSlowDownTime = 7f;
        [BoxGroup("SlowDown"), SerializeField] private Ease endGameSlowDownEase;

        [Button]
        private IEnumerator GameSlowDown() {
            yield return DOTween
                .To(() => Time.timeScale,
                    x => Time.timeScale = x,
                    endGameSlowDownFinalTimeScaleValue,
                    endGameSlowDownTime)
                .SetEase(endGameSlowDownEase)
                .OnUpdate(() => { Debug.Log(Time.timeScale); });
        }
    }
}