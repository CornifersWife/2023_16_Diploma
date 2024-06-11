using System;
using Scenes.Irys_is_doing_her_best.Scripts.My.Board;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Cards {
    public class CardDragging : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
        private Vector3 originalPosition;
        private CanvasGroup canvasGroup;
        public float dropDistanceThreshold = 100f;


        private void Awake() {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void OnBeginDrag(PointerEventData eventData) {
            originalPosition = this.transform.position;
            transform.position = eventData.position;
        }

        public void OnDrag(PointerEventData eventData) {
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData) {
            canvasGroup.blocksRaycasts = true;
        }
    }

}