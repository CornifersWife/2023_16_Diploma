using UnityEngine;
using UnityEngine.UI;

namespace CardBattles.Character.Mana.Additional {
    public class ManaPoint : MonoBehaviour {
        public Image image;
        public bool isAvailable = true;
        private void Awake() {
            image = GetComponent<Image>();
        }
    }
}