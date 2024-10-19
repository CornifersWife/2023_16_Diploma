using System.Collections.Generic;
using Esper.ESave;
using UnityEngine;

namespace SaveSystem.SaveData {
    public class LevelDataHandling:MonoBehaviour, ISavable {
        private List<GameObject> allItems;
        private List<GameObject> allWalls;

        private const string ItemSaveID = "Collectible item ";
        private const string WallSaveID = "Wall ";

        private void Awake() {
            //TODO read all collectible items and walls in the world
        }
        
        public void PopulateSaveData(SaveFile saveFile) {
            
        }

        public void LoadSaveData(SaveFile saveFile) {
            
        }
    }
}