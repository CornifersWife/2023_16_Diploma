/***
 * MIT License

Copyright (c) 2023 Shaped by Rain Studios

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of 
the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO 
THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS 
OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR 
OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 ***/

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
                QuestStep questStep = Object.Instantiate(questStepPrefab, parentTransform).GetComponent<QuestStep>();
                questStep.InitializeQuestStep(info.id);
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