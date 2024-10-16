using System.Collections.Generic;
using System.Linq;
using Esper.ESave;
using UnityEngine;

namespace SaveSystem {
    public class SaveManager: MonoBehaviour {
        private SaveData.SaveData saveData;
        private List<ISavable> savableObjects;
        
        private SaveFileSetup saveFileSetup;
        private SaveFile saveFile;

        public static SaveManager Instance;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            else {
                Destroy(this.gameObject);
            }
            
            saveFileSetup = GetComponent<SaveFileSetup>();
            saveFile = saveFileSetup.GetSaveFile();
            if (saveFile.isOperationOngoing) {
                saveFile.operation.onOperationEnded.AddListener(LoadGame);
            }
            else {
                LoadGame();
            }
        }

        private void Start() {
            savableObjects = FindAllSavableObjects();
            LoadGame();
        }

        public void SaveGame() {
            foreach (ISavable savableObject in savableObjects) {
                savableObject.PopulateSaveData(saveData);
            }
            
        }
        
        public void LoadGame() {
            saveData = null;

            if (saveData == null) {
                saveData = new SaveData.SaveData();
            }
            else {
                foreach (ISavable savableObject in savableObjects) {
                    savableObject.LoadSaveData(saveData);
                }
            }
        }

        private List<ISavable> FindAllSavableObjects() {
            IEnumerable<ISavable> objects = FindObjectsOfType<MonoBehaviour>()
                .OfType<ISavable>();
            return new List<ISavable>(objects);
        }
    }
}