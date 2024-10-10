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

        private void StartQuest(string id) {
            
        }
        
        private void AdvanceQuest(string id) {
            
        }
        
        private void FinishQuest(string id) {
            
        }
        
        private Dictionary<string, Quest> CreateQuestsDict() {
            QuestInfoSO[] allQuests = Resources.LoadAll<QuestInfoSO>("Quests");
            Dictionary<string, Quest> idToQuestDict = new Dictionary<string, Quest>();
            
            foreach (QuestInfoSO questInfo in allQuests) {
                idToQuestDict.Add(questInfo.id, new Quest(questInfo));
            }
            
            return idToQuestDict;
        }

        private Quest GetQuestById(string id) {
            return questsDict[id];
        }
    }
}