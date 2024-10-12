using System;

namespace Items {
    public class ItemEvents {
        public event Action OnItemCollected;
        public void ItemCollected() {
            OnItemCollected?.Invoke();
        }
    }
}