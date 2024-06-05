using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Cards {
    public class CardAnimation : MonoBehaviour {
        public bool inAnimation = false;
        [SerializeField] public bool isPlayers = false;

        [Header("Drawing")] [SerializeField] public Vector3 showPosition;

        [SerializeField] public Vector3 showScale;


        [SerializeField] private Ease playerToShow;
        [Range(0, 1)] [SerializeField] private float timeToShow = 0.5f;

        [Space] [SerializeField] private Ease showToHand;
        [Range(0, 1)] [SerializeField] private float timeToHand = 0.5f;

        [Space] [SerializeField] private Ease enemyToHand;
        [Range(0, 1)] [SerializeField] private float enemyDraw = 0.5f;

        private void Awake() {
            isPlayers = GetComponent<Card>().isPlayers;
        }

        public void DrawAnimation(Vector3 finalPosition) {
            StartCoroutine(isPlayers
                ? PlayerDrawAnimationCoroutine(finalPosition)
                : EnemyDrawAnimationCoroutine(finalPosition));
        }

        private IEnumerator PlayerDrawAnimationCoroutine(Vector3 finalPosition) {
            yield return new WaitUntil(() => inAnimation == false);
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
            inAnimation = true;
        }

        private IEnumerator EnemyDrawAnimationCoroutine(Vector3 finalPosition) {
            yield return new WaitUntil(() => inAnimation == false);
            yield return transform
                    .DOMove(finalPosition, enemyDraw)
                    .SetEase(enemyToHand)
                    .WaitForCompletion(true);
            inAnimation = true;
        }

        [Header("Hand")] [SerializeField] private float handMoveAnimationTime = 0.3f;

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