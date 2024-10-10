using QuestSystem;
using UnityEngine;

namespace Events {
    public class GameEventsManager: MonoBehaviour {
        public static GameEventsManager Instance;

        [Header("Events")] 
        public QuestEvents QuestEvents;

        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
            } else {
                Instance = this;
            }
            
            //initializing events
            QuestEvents = new QuestEvents();
        }
    }
}