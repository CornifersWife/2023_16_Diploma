using CardBattles.Interfaces.InterfaceObjects;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CardBattles.CardScripts.temp {
    public class CardPlayTarget : PlayerEnemyMonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IDropHandler {
         
        public void HoverOver() {
            
        }
        public void Highlight() {
            
        }

        public void OnPointerEnter(PointerEventData eventData) {
            if (!eventData.pointerDrag.TryGetComponent
                    (typeof(Card), out var draggedCard)) {
                Debug.Log($"{name}, dropped object was not a card");
                return;
            }
            
        }

        public void OnPointerExit(PointerEventData eventData) {
            throw new System.NotImplementedException();
        }

        public void OnDrop(PointerEventData eventData) {
            throw new System.NotImplementedException();
        }
    }
}