using NaughtyAttributes;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Cards {
    public class CardDragging : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;
        private Vector2 originalPosition;
        public bool isPlaced;
        
        [BoxGroup("Controls")]
        [SerializeField]
        private float dropDistanceThreshold = 100f;

        [BoxGroup("make it pretty")] [SerializeField]
        private float alphaDuringDragging = 0.8f;

        private void Awake() {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void OnBeginDrag(PointerEventData eventData) {
            canvasGroup.alpha = alphaDuringDragging;
            canvasGroup.blocksRaycasts = false;

            rectTransform.anchoredPosition += eventData.delta;
        }

        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData) {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;

            if (isPlaced)
                rectTransform.position = eventData.position;
            
        }
    }

}