using UnityEngine;

namespace QuestSystem {
    public class Quest {
        public QuestInfoSO info;
        public QuestState state;
        private int currentQuestStepIndex;

        public Quest(QuestInfoSO questInfo) {
            this.info = questInfo;
            this.state = QuestState.REQUIREMENTS_NOT_MET;
            this.currentQuestStepIndex = 0;
        }

        public void MoveToNextStep() {
            currentQuestStepIndex++;
        }

        public bool CurrentStepExists() {
            return (currentQuestStepIndex < info.questSteps.Length);
        }

        public void InstantiateCurrentQuestStep(Transform parentTransform) {
            GameObject questStepPrefab = GetCurrentQuestStepPrefab();
            if (questStepPrefab != null) {
                Object.Instantiate(questStepPrefab, parentTransform);
            }
        }

        private GameObject GetCurrentQuestStepPrefab() {
            GameObject questStepPrefab = null;
            if (CurrentStepExists()) {
                questStepPrefab = info.questSteps[currentQuestStepIndex];
            }
            else {
                Debug.LogWarning("Quest step index was out of range: questId = " +
                                 info.id + " stepIndex = " + currentQuestStepIndex);
            }
            return questStepPrefab;
        }
    }
}