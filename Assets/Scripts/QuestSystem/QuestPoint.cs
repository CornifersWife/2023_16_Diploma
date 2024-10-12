using Events;
using UnityEngine;
using UnityEngine.EventSystems;

namespace QuestSystem {
    public class QuestPoint: MonoBehaviour, IPointerClickHandler {
        [Header("Quest")]
        [SerializeField] private QuestInfoSO questInfo;

        [Header("Config")] 
        [SerializeField] private bool startPoint = false;
        [SerializeField] private bool finishPoint = false;
        
        private string questId;
        private QuestState currentQuestState;

        private void Awake() {
            questId = questInfo.id;
        }

        private void OnEnable() {
            GameEventsManager.Instance.QuestEvents.OnQuestStateChange += QuestStateChange;
        }
        
        private void OnDisable() {
            GameEventsManager.Instance.QuestEvents.OnQuestStateChange -= QuestStateChange;
        }
        
        public void OnPointerClick(PointerEventData eventData) {
            if(currentQuestState.Equals(QuestState.CAN_START) && startPoint)
                GameEventsManager.Instance.QuestEvents.StartQuest(questId);
            else if(currentQuestState.Equals(QuestState.CAN_FINISH) && finishPoint)
                GameEventsManager.Instance.QuestEvents.FinishQuest(questId);
        }

        private void QuestStateChange(Quest quest) {
            if (quest.info.id.Equals(questId)) {
                currentQuestState = quest.state;
            }
        }
    }
}