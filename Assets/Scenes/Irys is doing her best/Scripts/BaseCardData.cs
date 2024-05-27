using UnityEngine;

namespace ScriptableObjects {
    public abstract class BaseCardData : ScriptableObject {
        [HideInInspector]public int cost = 1;
        public string cardName;
        public Sprite cardImage;
        public bool belongsToPlayer = false;

        public abstract void Play();

        public object GetCardSetCards() {
            throw new System.NotImplementedException();
        }
    }
}

