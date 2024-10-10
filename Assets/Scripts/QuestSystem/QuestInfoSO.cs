using UnityEngine;

namespace QuestSystem {
    [CreateAssetMenu(fileName = "QuestInfoSO", menuName = "Quests/QuestInfoSO")]
    public class QuestInfoSO: ScriptableObject {
        public string id;
        
        [Header("General")]
        public string displayName;
        [TextArea(1, 5)]
        public string questDescription;
        
        [Header("Requirements")]
        public QuestInfoSO[] questPrerequisites;

        [Header("Steps")] 
        public GameObject[] questSteps;

        [Header("Rewards")] 
        public GameObject[] questRewards;

        private void OnValidate() {
            #if UNITY_EDITOR
            id = this.name;
            UnityEditor.EditorUtility.SetDirty(this);
            #endif
        }
    }
}