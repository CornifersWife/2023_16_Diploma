using System;
using Scenes.Irys_is_doing_her_best.Scripts.My.Board;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Cards {
    //TODO FIX IT IT DOESNT WORK
    public class CardDragging : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
        private Vector3 originalPosition;
        private CanvasGroup canvasGroup; 
//        private bool isDroppedInValidSpot = false;
        public float dropDistanceThreshold = 100f; // Adjust this threshold as needed

        //public delegate CardSpot[] RequestCardSpots(object sender, EventArgs e);

      //  public event RequestCardSpots cardSpots;

        
        private void Awake() {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void OnBeginDrag(PointerEventData eventData) {
            originalPosition = transform.position;
            canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData) {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData) {
            canvasGroup.blocksRaycasts = true;

            CardSpot nearestSpot = FindNearestCardSpot();
            if (nearestSpot != null) {
                transform.position = nearestSpot.transform.position;
                // You can add additional logic here to attach the card to the spot, if necessary
             //   isDroppedInValidSpot = true;
            } else {
                transform.position = originalPosition;
           //     isDroppedInValidSpot = false;
            }
        }

        private CardSpot FindNearestCardSpot() {
            CardSpot[] cardSpots = null;
            CardSpot nearestSpot = null;
            float minDistance = dropDistanceThreshold;

            foreach (CardSpot spot in cardSpots) {
                float distance = Vector3.Distance(transform.position, spot.transform.position);
                if (distance < minDistance) {
                    minDistance = distance;
                    nearestSpot = spot;
                }
            }

            return nearestSpot;
        }


    }
}