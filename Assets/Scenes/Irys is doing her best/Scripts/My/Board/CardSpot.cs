using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Board {
    public class CardSpot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {
        private Image image;
        public Cards.Card card;
        private bool isPlayers;
        [ShowNativeProperty] public bool IsPlayers => isPlayers;

        
        
        private void Awake() {
            isPlayers = CompareTag("Player");

            image = GetComponent<Image>();
        }

        public void OnDrop(PointerEventData eventData) {

            if (!isPlayers) {
                return;
            }

            if (card is not null) {
                Debug.Log($"{name} already has a card");
                return;
            }

            Debug.Log(eventData.pointerDrag.name);

            if (eventData.pointerDrag is null) {
                Debug.Log($"{name}, was dropped a null object");
                return;
            }
            if (!eventData.pointerDrag.TryGetComponent
                    (typeof(Cards.Card), out var draggedCard)) {
                Debug.Log($"{name}, dropped object was not a card");
                return;
            }
            CharacterManager.PlayACard((Cards.Card)draggedCard, this);
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
            image.DOColor(Color.white, 0.1f).SetEase(Ease.InOutQuad);
        }
    }
}