using System.Collections;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Cards {
    public class CardDragging : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;
        private Vector2 originalPosition;
        public bool droppedOnSlot;

        [SerializeField] private Card card;


        [BoxGroup("make it pretty")] [SerializeField]
        private float alphaDuringDragging = 0.8f;

        private void Awake() {
            card = GetComponent<Card>();
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            enabled = false;
        }

        public void OnBeginDrag(PointerEventData eventData) {
            if (!enabled || !card.isPlayers || droppedOnSlot) {
                enabled = false;
                return;
            }

            canvasGroup.alpha = alphaDuringDragging;
            canvasGroup.blocksRaycasts = false;
            originalPosition = transform.position;
        }

        public void OnDrag(PointerEventData eventData) {
            if (!enabled) {
                return;
            }

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform.parent as RectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out var localPointerPosition
            );
            rectTransform.anchoredPosition = localPointerPosition;
        }

        [SerializeField] private float snapBackTime = 0.1f;
        [SerializeField] private float animationFasterIfDistanceLessThan = 700f;

        public void OnEndDrag(PointerEventData eventData) {
            if (!enabled) {
                return;
            }

            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;

            if (droppedOnSlot) {
                enabled = false;
            }
            else {
                SnapBack();
            }
        }

        /*private IEnumerator CheckIfPlaced() {
            yield return new WaitForSeconds(0.5f);
            if (droppedOnSlot) {
                enabled = false;
            }
            else {
                SnapBack();
            }
        }*/

        private void SnapBack() {
            var distance = Vector3.Distance(transform.position, originalPosition);
            var animationTime = snapBackTime;
            if (distance < animationFasterIfDistanceLessThan) {
                animationTime *= distance / animationFasterIfDistanceLessThan;
            }

            transform
                .DOMove(originalPosition,
                    animationTime)
                .SetEase(Ease.InOutSine);
        }
    }
}