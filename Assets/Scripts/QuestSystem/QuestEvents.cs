using System;
namespace QuestSystem {
    public class QuestEvents {
        public event Action<string> OnStartQuest;
        public void StartQuest(string id) {
            OnStartQuest?.Invoke(id);
        }
        
        public event Action<string> OnAdvanceQuest;
        public void AdvanceQuest(string id) {
            OnAdvanceQuest?.Invoke(id);
        }
        
        public event Action<string> OnFinishQuest;
        public void FinishQuest(string id) {
            OnFinishQuest?.Invoke(id);
        }
        
        public event Action<Quest> OnQuestStateChange;
        public void QuestStateChange(Quest quest) {
            OnQuestStateChange?.Invoke(quest);
        }
    }
}