using System.Collections.Generic;
using System.Linq;
using Esper.ESave;
using NPC;
using ScriptableObjects.Dialogue;
using UnityEngine;

namespace SaveSystem.SaveData {
    public class NPCDataHandling: MonoBehaviour, ISavable {
        private List<TalkableNPC> allNPCs;
        private const string NpcSaveID = "NPC data ";

        private void Awake() {
            IEnumerable<TalkableNPC> objects = FindObjectsOfType<MonoBehaviour>(true)
                .OfType<TalkableNPC>();
            allNPCs = new List<TalkableNPC>(objects);
        }
        
        public void PopulateSaveData(SaveFile saveFile) {
            int i = 0;
            foreach (TalkableNPC npc in allNPCs) {
                int j = 0;
                foreach (DialogueText dialogue in npc.dialogue) {
                    j++;
                }
                saveFile.AddOrUpdateData(NpcSaveID + i, j);
                i++;
            }
        }

        public void LoadSaveData(SaveFile saveFile) {
            if (!saveFile.HasData(NpcSaveID + 0))
                return;
            
            int i = 0;
            foreach (TalkableNPC npc in allNPCs) {
                int j = npc.dialogue.Count;
                int dialogueIndex = saveFile.GetData<int>(NpcSaveID + i);
                while (j != dialogueIndex) {
                    npc.dialogue.Remove(npc.dialogue[0]);
                    j--;
                }
                i++;
            }
        }
    }
}