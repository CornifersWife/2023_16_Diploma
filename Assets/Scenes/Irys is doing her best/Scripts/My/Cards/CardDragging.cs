using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Cards {
    public class CardDragging : MonoBehaviour,IDroppable,IBeginDragHandler {
        public bool IsDroppable() {
            throw new System.NotImplementedException();
        }

        public void OnBeginDrag(PointerEventData eventData) {
            throw new System.NotImplementedException();
        }
    }
}