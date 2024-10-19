using System.Collections.Generic;
using Esper.ESave;
using UnityEngine;

namespace SaveSystem.SaveData {
    public class InventoryDataHandling:MonoBehaviour, ISavable {
        private List<GameObject> allItems;
        private List<GameObject> allCardSets;
        private List<GameObject> allDeckCardSets;

        private const string ItemSaveID = "Inventory item ";
        private const string CardSetSaveID = "Inventory Card Set ";
        private const string DeckSaveID = "Inventory deck ";

        private void Awake() {
            //TODO read all items and card sets
        }
        
        
        public void PopulateSaveData(SaveFile saveFile) {
            
        }

        public void LoadSaveData(SaveFile saveFile) {
            
        }
    }
}