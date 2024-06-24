using System;
using System.Collections;
using DG.Tweening;
using NaughtyAttributes;
using Scenes.Irys_is_doing_her_best.Scripts.My.Board;
using Scenes.Irys_is_doing_her_best.Scripts.My.Interfaces;
using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Cards {
    public class CardAnimation : MonoBehaviour {
        private bool inAnimation = false; //TODO CHECK IF NEEDED
        private bool isWaiting = false; //TODO CHECK IF NEEDED
        [NonSerialized] public bool isPlayers = false;

        private float scaleInHand;


        [Header("Show")] [Foldout("Draw"), SerializeField]
        public Vector3 showPosition;

        [Foldout("Draw"), SerializeField] public Vector3 showScale = Vector3.one;


        [Space, Header("Deck to show")] [Foldout("Draw"), SerializeField]
        private Ease playerToShow;

        [Foldout("Draw"), Range(0, 1), SerializeField]
        private float timeToShow = 0.5f;


        [Space, Header("Show to hand")] [Foldout("Draw"), SerializeField]
        private Ease showToHand;

        [Foldout("Draw"), Range(0, 1), SerializeField]
        private float timeToHand = 0.5f;


        [Space, Header("Enemy Draw")] [Foldout("Draw"), SerializeField]
        private Ease enemyToHand;

        [Foldout("Draw"), Range(0, 1), SerializeField]
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
            sequence.Play();
            yield return sequence.WaitForCompletion();
            inAnimation = false;
        }


        [Foldout("Move To")] [SerializeField] private float moveToAnimationTime = 0.3f;
        [Foldout("Move To")] [SerializeField] private Ease moveToEase;


        public IEnumerator MoveTo(Vector3 vector3) {
            yield return new WaitUntil(() => inAnimation == false);
            inAnimation = true;
            yield return transform
                .DOMove(vector3, moveToAnimationTime)
                .SetEase(Ease.InOutSine)
                .WaitForCompletion(true);
            inAnimation = false;
        }

        [Foldout("Play Card")] [InfoBox("Currently only used when enemy plays a card")] [SerializeField]
        private float playCardTime = 0.4f;

        [Foldout("Play Card")] [SerializeField]
        private Ease playCardEase = Ease.InOutSine;

        public IEnumerator PlayAnimation(CardSpot cardSpot) {
            yield return transform
                .DOMove(
                    cardSpot.transform.position,
                    playCardTime)
                .SetEase(playCardEase);
        }

        [Space, Header("MoveTo")] [Foldout("Attack Animation"), SerializeField]
        private float attackMoveToTime;

        [Foldout("Attack Animation"), SerializeField]
        private Ease attackMoveToEase;


        [Space, Header("KnockBack")] [Foldout("Attack Animation"), SerializeField]
        private float attackKnockBackAmount;

        [Foldout("Attack Animation"), SerializeField]
        private float attackKnockBackTime;

        [Foldout("Attack Animation"), SerializeField]
        private Ease attackKnockBackEase;

        [Space, Header("MoveBack")] [Foldout("Attack Animation"), SerializeField]
        private float attackMoveBackTime;

        [Foldout("Attack Animation"), SerializeField]
        private Ease attackMoveBackEase;

        //USE LIKE A STATIC METHOD 
        public IEnumerator AttackAnimation(IAttacker attacker, IDamageable damageable) {
            var attack = attacker.GetAttack();
            var attackerTransform = attacker.GetTransform();
            var originalPosition = attackerTransform.position;
            var targetPosition = damageable.GetTransform().position;

            yield return AttackMoveToTarget(attackerTransform, targetPosition);

            damageable.TakeDamage(attack);

            yield return AttackKnockback(attackerTransform);

            yield return AttackMoveBack(attackerTransform, originalPosition);


            yield return null;
        }

        private IEnumerator AttackMoveToTarget(Transform attackerTransform, Vector3 finalPosition) {
            var moveToTarget = attackerTransform
                .DOMove(
                    finalPosition,
                    attackMoveToTime)
                .SetEase(attackMoveToEase)
                .WaitForCompletion(true);
            yield return moveToTarget;
        }

        private IEnumerator AttackKnockback(Transform attackerTransform) {
            var knockbackPosition = attackerTransform.position;
            knockbackPosition -= new Vector3(0, attackKnockBackAmount, 0);
            var knockBack = attackerTransform
                .DOMove(
                    knockbackPosition,
                    attackKnockBackTime)
                .SetEase(attackKnockBackEase)
                .WaitForCompletion(true);
            yield return knockBack;
        }

        private IEnumerator AttackMoveBack(Transform attackerTransform, Vector3 originalPosition) {
            var moveBack = attackerTransform
                .DOMove(
                    originalPosition,
                    attackMoveBackTime)
                .SetEase(attackMoveBackEase)
                .WaitForCompletion(true);
            yield return moveBack;
        }
    }
}