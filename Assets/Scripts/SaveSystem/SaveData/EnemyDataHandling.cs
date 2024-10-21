using System.Collections.Generic;
using System.Linq;
using Esper.ESave;
using NPC;
using ScriptableObjects.Dialogue;
using UnityEngine;

namespace SaveSystem.SaveData {
    public class EnemyDataHandling: MonoBehaviour, ISavable {
        private List<EnemySM> allEnemies;
        private const string EnemySaveID = "Enemy data ";

        private void Awake() {
            IEnumerable<EnemySM> objects = FindObjectsOfType<MonoBehaviour>(true)
                .OfType<EnemySM>();
            allEnemies = new List<EnemySM>(objects);
        }
        
        public void PopulateSaveData(SaveFile saveFile) {
            int i = 0;
            foreach (EnemySM enemy in allEnemies) {
                saveFile.AddOrUpdateData(EnemySaveID + i, enemy.GetState());
                i++;
            }
        }

        public void LoadSaveData(SaveFile saveFile) {
            if (!saveFile.HasData(EnemySaveID + 0))
                return;
            
            int i = 0;
            foreach (EnemySM enemy in allEnemies) {
                EnemyState state = (EnemyState)saveFile.GetData<int>(EnemySaveID + i);
                enemy.ChangeState(state);
                i++;
            }
        }
    }
}