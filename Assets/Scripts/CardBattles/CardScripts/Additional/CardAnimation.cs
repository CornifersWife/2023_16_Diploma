using System;
using System.Collections;
using Audio;
using CardBattles.Interfaces;
using CardBattles.Interfaces.InterfaceObjects;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace CardBattles.CardScripts.Additional {
    public class CardAnimation : PlayerEnemyMonoBehaviour {
     
        [Header("Show")] [Foldout("Draw"), SerializeField]
        public Vector3 showPosition;

        [Foldout("Draw"), SerializeField]
        public Vector3 showScale = Vector3.one;


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

        public IEnumerator DrawAnimation(Vector3 finalPosition) {
            var drawAnimation = StartCoroutine(IsPlayers
                ? PlayerDrawAnimationCoroutine(finalPosition)
                : EnemyDrawAnimationCoroutine(finalPosition));
            yield return drawAnimation;
        }

        private IEnumerator PlayerDrawAnimationCoroutine(Vector3 finalPosition) {
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
                    .DOScale(Vector3.one * CardDisplay.scaleInHand, timeToHand)
                    .SetEase(showToHand));

            sequence.Play();
            yield return sequence.WaitForCompletion();
        }

        private IEnumerator EnemyDrawAnimationCoroutine(Vector3 finalPosition) {
            var sequence = DOTween.Sequence();
            sequence.Append(
                transform
                    .DOMove(finalPosition, enemyDraw)
                    .SetEase(enemyToHand));
            sequence.Join(
                transform
                    .DOScale(Vector3.one * CardDisplay.scaleInHand
                        , enemyDraw));
            sequence.Play();
            yield return sequence.WaitForCompletion();
        }


        [Foldout("Move To")] [SerializeField]
        private float moveToAnimationTime = 0.3f;

        [Foldout("Move To")] [SerializeField]
        private Ease moveToEase;


        public IEnumerator MoveTo(Vector3 vector3) {
            yield return transform
                .DOMove(vector3, moveToAnimationTime)
                .SetEase(Ease.InOutSine)
                .WaitForCompletion(true);
        }

        [Foldout("Play Card")] [InfoBox("Currently only used when enemy plays a card")] [SerializeField]
        private float playCardTime = 0.4f;

        [Foldout("Play Card")] [SerializeField]
        private Ease playCardEase = Ease.InOutSine;

        public IEnumerator PlayToCardSpotAnimation(CardSpot cardSpot) {
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
        private float attackKnockBackShakeStrength;

        [Foldout("Attack Animation"), SerializeField]
        private float attackKnockBackTime;

        [Foldout("Attack Animation"), SerializeField]
        private Ease attackKnockBackEase;

        [Space, Header("MoveBack")] [Foldout("Attack Animation"), SerializeField]
        private float attackMoveBackTime;

        [Foldout("Attack Animation"), SerializeField]
        private Ease attackMoveBackEase;

        //TODO Add distance scaling to animaation
        //USE LIKE A STATIC METHOD 
        public IEnumerator AttackAnimation(IAttacker attacker, IDamageable damageable) {
            var attack = attacker.GetAttack();
            var attackerTransform = attacker.GetTransform();
            var originalPosition = attackerTransform.position;
            var targetPosition = damageable.GetTransform().position;
            var moveDirection = (targetPosition - originalPosition).normalized;
            yield return AttackMoveToTarget(attackerTransform, targetPosition);

            damageable.TakeDamage(attack);

            //TODO add variable to determine how much time to stop for at a target
            yield return new WaitForEndOfFrame();
            yield return AttackKnockback(attackerTransform, moveDirection);

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

        private IEnumerator AttackKnockback(Transform attackerTransform, Vector3 moveDirection) {
            //TODO for some reason this doenst animate both shake and move at once
            //var move = StartCoroutine(AttackKnockbackMove(attackerTransform, moveDirection));
            //var shake = StartCoroutine(AttackKnockbackShake(attackerTransform));
            //TODO so i had to do this
            var sequence = DOTween.Sequence();

            var knockbackPosition = attackerTransform.position - (moveDirection * attackKnockBackAmount);


            sequence.Join(attackerTransform
                .DOMove(
                    knockbackPosition,
                    attackKnockBackTime)
                .SetEase(attackKnockBackEase));


            /*
            sequence.Join(attackerTransform
                .DOShakePosition(
                    attackKnockBackTime,
                    attackKnockBackShakeStrength)
                .SetEase(attackMoveToEase));*/


            sequence.Play();
            yield return sequence.WaitForCompletion();
        }

        private IEnumerator AttackKnockbackMove(Transform attackerTransform, Vector3 moveDirection) {
            var knockbackPosition = attackerTransform.position - moveDirection * attackKnockBackAmount;

            var knockBack = attackerTransform
                .DOMove(
                    knockbackPosition,
                    attackKnockBackTime)
                .SetEase(attackKnockBackEase)
                .WaitForCompletion(true);
            yield return knockBack;
        }

        private IEnumerator AttackKnockbackShake(Transform attackerTransform) {
            var shake = attackerTransform
                .DOShakePosition(
                    attackKnockBackTime,
                    attackKnockBackShakeStrength)
                .SetEase(attackMoveToEase)
                .WaitForCompletion(true);
            yield return shake;
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

        //check if the card is dragged
        private IEnumerator CanBeAnimated() {
            throw new NotImplementedException();
        }

        public IEnumerator Die() {
            yield return null;
        }

        public IEnumerator TakeDamage() {
            yield return null;
        }

        public IEnumerator Play(Card card) {
            switch (card) {
                case Spell:
                    StartCoroutine(FadeOut(card.gameObject));
                    break;
            }

            yield return null;
        }

        [Space, Header("FadeOut"), Foldout("FadeOut"), SerializeField]
        private float cardFadeOutDuration = 0.7f;

        [Foldout("FadeOut"), SerializeField]
        private Ease cardFadeOutEase = Ease.InOutQuad;

        private IEnumerator FadeOut(GameObject cardGameObject) {
            if (!TryGetComponent(typeof(CanvasGroup), out var canvasGroup))
                yield break;

            yield return ((CanvasGroup)canvasGroup)
                .DOFade(0f, cardFadeOutDuration)
                .SetEase(cardFadeOutEase)
                .WaitForCompletion();
        }
    }
}