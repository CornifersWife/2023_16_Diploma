using System.Collections.Generic;
using System.Linq;
using Esper.ESave;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SaveSystem {
    public class SaveManager: MonoBehaviour {
        private SaveData.SaveData saveData;
        private List<ISavable> savableObjects;
        
        private SaveFileSetup saveFileSetup;
        private SaveFile saveFile;

        public static SaveManager Instance;

        private void OnEnable() {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable() {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
            savableObjects = FindAllSavableObjects();
            LoadGame();
        }

        private void Awake() {
            if (Instance != null) {
                Destroy(this.gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        private void Start() {
            saveFileSetup = GetComponent<SaveFileSetup>();
            saveFile = saveFileSetup.GetSaveFile();
            if (saveFile.isOperationOngoing) {
                saveFile.operation.onOperationEnded.AddListener(LoadGame);
            }
            else {
                LoadGame();
            }
        }

        public void NewGame() {
            saveData = new SaveData.SaveData();
        }

        public void SaveGame() {
            if (!HasSaveData()) {
                return;
            }
            
            foreach (ISavable savableObject in savableObjects) {
                savableObject.PopulateSaveData(saveData);
            }
            
        }
        
        public void LoadGame() {
            saveData = saveFile.GetData<SaveData.SaveData>()[0];
            
            if (!HasSaveData()) {
                return;
            }

            foreach (ISavable savableObject in savableObjects) {
                savableObject.LoadSaveData(saveData);
            }
        }

        private List<ISavable> FindAllSavableObjects() {
            IEnumerable<ISavable> objects = FindObjectsOfType<MonoBehaviour>(true)
                .OfType<ISavable>();
            return new List<ISavable>(objects);
        }

        public bool HasSaveData() {
            return saveData != null;
        }
    }
}