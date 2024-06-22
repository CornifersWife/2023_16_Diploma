using System;
using System.Collections;
using DG.Tweening;
using NaughtyAttributes;
using Scenes.Irys_is_doing_her_best.Scripts.My.Board;
using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Cards {
    public class CardAnimation : MonoBehaviour {
        private bool inAnimation = false; //TODO CHECK IF NEEDED
        private bool isWaiting = false; //TODO CHECK IF NEEDED
        [NonSerialized] public bool isPlayers = false;

        private float scaleInHand;


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


        private void Start() {
            isPlayers = CompareTag("Player");
            scaleInHand = GetComponent<CardDisplay>().scaleInHand;
        }

        public IEnumerator DrawAnimation(Vector3 finalPosition) {
            var drawAnimation = StartCoroutine(isPlayers
                ? PlayerDrawAnimationCoroutine(finalPosition)
                : EnemyDrawAnimationCoroutine(finalPosition));
            yield return drawAnimation;
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
           // yield return sequence.Play();

            //var sequence2 = DOTween.Sequence();
            sequence
                .Append(transform
                    .DOMove(finalPosition, timeToHand)
                    .SetEase(showToHand))
                .Join(transform
                    .DOScale(Vector3.one * scaleInHand, timeToHand)
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
            var sequence = DOTween.Sequence();
            sequence.Append(
                transform
                    .DOMove(finalPosition, enemyDraw)
                    .SetEase(enemyToHand));
            sequence.Join(
                transform
                    .DOScale(Vector3.one * scaleInHand
                        , enemyDraw));
            yield return sequence.Play();
            inAnimation = false;
        }


        [BoxGroup("Move To")] [SerializeField] private float moveToAnimationTime = 0.3f;
        [BoxGroup("Move To")] [SerializeField] private Ease moveToEase;


        public IEnumerator MoveTo(Vector3 vector3) {
            yield return new WaitUntil(() => inAnimation == false);
            inAnimation = true;
            yield return transform
                .DOMove(vector3, moveToAnimationTime)
                .SetEase(Ease.InOutSine)
                .WaitForCompletion(true);
            inAnimation = false;
        }

        [BoxGroup("Play Card")] [InfoBox("Currently only used when enemy plays a card")] [SerializeField]
        private float playCardTime = 0.4f;

        [BoxGroup("Play Card")] [SerializeField]
        private Ease playCardEase = Ease.InOutSine;

        public IEnumerator PlayAnimation(CardSpot cardSpot) {
            yield return transform
                .DOMove(
                    cardSpot.transform.position,
                    playCardTime)
                .SetEase(playCardEase);
        }
    }
}