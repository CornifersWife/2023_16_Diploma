using System;
using DG.Tweening;
using Scenes.Irys_is_doing_her_best.Scripts.My.Cards;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Board {
    public class CardSpot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {
        private Image image;
        public GameObject cardObject;

        public bool isPlayers { get; set; }

        private void Awake() {
            if (CompareTag("Player"))
                isPlayers = true;
            else {
                isPlayers = false;
            }

            image = GetComponent<Image>();
        }

        public void OnDrop(PointerEventData eventData) {
            Debug.Log(eventData.pointerDrag.name);

            if(!isPlayers)
                return;
            if(cardObject is not null)
                return;
            
            if (eventData.pointerDrag is null) {
                Debug.Log($"{name}, was dropped a null object");
                return;
            }
            if (!eventData.pointerDrag.TryGetComponent
                    (typeof(Cards.Card), out var card)) {
                Debug.Log($"{name}, dropped object was not a card");
                return;
            }
            CharacterManager.PlayACard((Cards.Card)card, this);
        }

        
        //TODO MAGIC NUMBER AND COLOR AND EASE
        public void OnPointerEnter(PointerEventData eventData) {
            if(!isPlayers)
                return;
            image.DOColor(Color.gray, 0.1f).SetEase(Ease.InOutQuad);
        }
        //TODO SAME AS PREV
        public void OnPointerExit(PointerEventData eventData) {
            if(!isPlayers)
                return;
            image.DOColor(Color.white, 0.1f).SetEase(Ease.InOutQuad);;
        }
    }
}