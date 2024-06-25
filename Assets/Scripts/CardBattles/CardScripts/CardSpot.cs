using CardBattles.Character;
using CardBattles.Interfaces;
using CardBattles.Interfaces.InterfaceObjects;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CardBattles.CardScripts {
    public class CardSpot : PlayerEnemyMonoBehaviour, ICardPlayTarget,IDropHandler, IPointerEnterHandler, IPointerExitHandler {
        private Image image;
        public Card card;

        
        
        private void Awake() {

            image = GetComponent<Image>();
        }

        public void OnDrop(PointerEventData eventData) {
            if (!IsPlayers) {
                return;
            }

            if (card is not null) {
                Debug.Log($"{name} already has a card");
                return;
            }
            
            if (eventData.pointerDrag is null) {
                Debug.Log($"{name}, was dropped a null object");
                return;
            }
            if (!eventData.pointerDrag.TryGetComponent
                    (typeof(Card), out var draggedCard)) {
                Debug.Log($"{name}, dropped object was not a card");
                return;
            }
            CharacterManager.PlayACard((Card)draggedCard, this);
        }

        
        //TODO MAGIC NUMBER AND COLOR AND EASE
        public void OnPointerEnter(PointerEventData eventData) {
            if(IsPlayers)
                return;
            image.DOColor(Color.gray, 0.1f).SetEase(Ease.InOutQuad);
        }
        //TODO SAME AS PREV
        public void OnPointerExit(PointerEventData eventData) {
            if(IsPlayers)
                return;
            image.DOColor(Color.white, 0.1f).SetEase(Ease.InOutQuad);
        }
    }
}