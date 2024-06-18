using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

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
            if (!enabled || !card.isPlayers) {
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

        public void OnEndDrag(PointerEventData eventData) {
            if (!enabled) {
                return;
            }

            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;

            if (droppedOnSlot)
                enabled = false;
            else {
                transform.position = originalPosition;
            }
        }
    }
}