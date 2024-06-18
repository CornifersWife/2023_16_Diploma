using System.Collections;
using DG.Tweening;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Cards {
    public class CardAnimation : MonoBehaviour {
        public bool inAnimation = false;
        public bool isWaiting = false;
        [SerializeField] public bool isPlayers = false;


        [BoxGroup("Show")] [SerializeField] public Vector3 showPosition;

        [BoxGroup("Show")] [SerializeField] public Vector3 showScale = Vector3.one;

        [BoxGroup("Deck to Show")] [SerializeField]
        private Ease playerToShow;

        [BoxGroup("Deck to Show")] [Range(0, 1)] [SerializeField]
        private float timeToShow = 0.5f;

        [BoxGroup("Show to hand")] [SerializeField]
        private Ease showToHand;

        [BoxGroup("Show to hand")] [Range(0, 1)] [SerializeField]
        private float timeToHand = 0.5f;

        [BoxGroup("EnemyDraw")] [SerializeField]
        private Ease enemyToHand;

        [BoxGroup("EnemyDraw")] [SerializeField] [Range(0, 1)]
        private float enemyDraw = 0.5f;


        private void Awake() {
            if (CompareTag("Player"))
                isPlayers = true;
            else {
                isPlayers = false;
            }
        }

        public void DrawAnimation(Vector3 finalPosition) {
            
            StartCoroutine(isPlayers
                ? PlayerDrawAnimationCoroutine(finalPosition)
                : EnemyDrawAnimationCoroutine(finalPosition));
        }

        private IEnumerator PlayerDrawAnimationCoroutine(Vector3 finalPosition) {
            yield return new WaitUntil(() => isWaiting == false);
            isWaiting = true;
            yield return new WaitUntil(() => inAnimation == false);
            inAnimation = true;
            isWaiting = false;
            var sequence = DOTween.Sequence();
            sequence
                .Append(transform
                    .DOMove(showPosition, timeToShow)
                    .SetEase(playerToShow))
                .Join(transform
                    .DOScale(showScale, timeToShow)
                    .SetEase(playerToShow));
            sequence
                .Append(transform
                    .DOMove(finalPosition, timeToHand)
                    .SetEase(showToHand))
                .Join(transform
                    .DOScale(Vector3.one, timeToHand)
                    .SetEase(showToHand));

            sequence.Play();
            yield return sequence.WaitForCompletion();
            inAnimation = false;
        }

        private IEnumerator EnemyDrawAnimationCoroutine(Vector3 finalPosition) {
            yield return new WaitUntil(() => isWaiting == false);
            isWaiting = true;
            yield return new WaitUntil(() => inAnimation == false);
            inAnimation = true;
            isWaiting = false;
            yield return transform
                .DOMove(finalPosition, enemyDraw)
                .SetEase(enemyToHand)
                .WaitForCompletion(true);
            inAnimation = false;
        }


        [SerializeField] private float handMoveAnimationTime = 0.3f;
        [SerializeField] private Ease ease;

        public void MoveTo(Vector3 vector3) {
            StartCoroutine(MoveToCoroutine(vector3));
        }

        private IEnumerator MoveToCoroutine(Vector3 vector3) {
            yield return new WaitUntil(() => inAnimation == false);
            inAnimation = true;
            yield return transform
                .DOMove(vector3, handMoveAnimationTime)
                .SetEase(Ease.InOutSine)
                .WaitForCompletion(true);
            inAnimation = false;
        }
    }
}