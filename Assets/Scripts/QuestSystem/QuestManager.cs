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

using System.Collections.Generic;
using Events;
using UnityEngine;

namespace QuestSystem {
    public class QuestManager: MonoBehaviour {
        private Dictionary<string, Quest> questsDict;

        private void Awake() {
            questsDict = CreateQuestsDict();
        }

        private void OnEnable() {
            GameEventsManager.Instance.QuestEvents.OnStartQuest += StartQuest;
            GameEventsManager.Instance.QuestEvents.OnAdvanceQuest += AdvanceQuest;
            GameEventsManager.Instance.QuestEvents.OnFinishQuest += FinishQuest;
        }

        private void OnDisable() {
            GameEventsManager.Instance.QuestEvents.OnStartQuest -= StartQuest;
            GameEventsManager.Instance.QuestEvents.OnAdvanceQuest -= AdvanceQuest;
            GameEventsManager.Instance.QuestEvents.OnFinishQuest -= FinishQuest;
        }

        private void Start() {
            foreach (Quest quest in questsDict.Values) {
                GameEventsManager.Instance.QuestEvents.QuestStateChange(quest);
            }
        }

        private void Update() {
            foreach (Quest quest in questsDict.Values) {
                if (quest.state == QuestState.REQUIREMENTS_NOT_MET && CheckRequirementsMet(quest)) {
                    ChangeQuestState(quest.info.id, QuestState.CAN_START);
                }
            }
        }

        private void ChangeQuestState(string id, QuestState state) {
            Quest quest = GetQuestById(id);
            quest.state = state;
            GameEventsManager.Instance.QuestEvents.QuestStateChange(quest);
        }

        private bool CheckRequirementsMet(Quest quest) {
            bool meetsRequirements = true;
            foreach (QuestInfoSO questPrerequisite in quest.info.questPrerequisites) {
                if (GetQuestById(questPrerequisite.id).state != QuestState.FINISHED) {
                    meetsRequirements = false;
                    break;
                }
            }
            return meetsRequirements;
        }

        private void StartQuest(string id) {
            Quest quest = GetQuestById(id);
            quest.InstantiateCurrentQuestStep(this.transform);
            ChangeQuestState(quest.info.id, QuestState.IN_PROGRESS);
        }
        
        private void AdvanceQuest(string id) {
            Quest quest = GetQuestById(id);
            quest.MoveToNextStep();
            if (quest.CurrentStepExists()) {
                quest.InstantiateCurrentQuestStep(this.transform);
            }
            else {
                ChangeQuestState(quest.info.id, QuestState.CAN_FINISH);
            }
        }
        
        private void FinishQuest(string id) {
            Quest quest = GetQuestById(id);
            ClaimRewards(quest);
            ChangeQuestState(quest.info.id, QuestState.FINISHED);
        }

        private void ClaimRewards(Quest quest) {
            //TODO implement rewards
            Debug.Log("Good job!");
        }
        
        private Dictionary<string, Quest> CreateQuestsDict() {
            QuestInfoSO[] allQuests = Resources.LoadAll<QuestInfoSO>("Quests");
            Dictionary<string, Quest> idToQuestDict = new Dictionary<string, Quest>();
            
            foreach (QuestInfoSO questInfo in allQuests) {
                idToQuestDict.Add(questInfo.id, new Quest(questInfo));
            }
            
            return idToQuestDict;
        }

        public Quest GetQuestById(string id) {
            return questsDict[id];
        }
    }
}