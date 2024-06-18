using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Board {
    public class CardSpot : MonoBehaviour, IDropHandler {
        public Cards.Card card;
        
        public bool isPlayer { get; set; }

        private void Awake() {
            if (CompareTag("Player"))
                isPlayer = true;
            else {
                isPlayer = false;
            }
        }

        public void OnDrop(PointerEventData eventData) {
            Debug.Log(eventData.pointerDrag);
            if(!isPlayer)
                return;
        }
    }
}