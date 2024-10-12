using Events;
using UnityEngine;

namespace QuestSystem {
    public abstract class QuestStep: MonoBehaviour {
        private string questId;
        private bool isFinished = false;

        public void InitializeQuestStep(string questId) {
            this.questId = questId;
        }

        protected void FinishQuestStep() {
            if (!isFinished) {
                isFinished = true;
                GameEventsManager.Instance.QuestEvents.AdvanceQuest(questId);
                Destroy(this.gameObject);
            }
        }
    }
}