using System.Collections.Generic;
using System.Linq;
using Esper.ESave;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SaveSystem {
    public class SaveManager: MonoBehaviour {
        private SaveFileSetup saveFileSetup;
        private SaveFile saveFile;
        private List<ISavable> savableObjects;

        private const string InitialSaveDataID = "Initial save data";

        public static SaveManager Instance;

        private void OnEnable() {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable() {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
            Debug.Log("Scene loaded");
            saveFileSetup = GetComponent<SaveFileSetup>();
            saveFile = saveFileSetup.GetSaveFile();
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

        public void NewGame() {
            saveFile.EmptyFile();
            saveFile.AddOrUpdateData(InitialSaveDataID, 0);
            saveFile.Save();
        }

        public void SaveGame() {
            if (!HasSaveData()) {
                Debug.Log("Tried saving but no data");
                return;
            }
            
            foreach (ISavable savableObject in savableObjects) {
                savableObject.PopulateSaveData(saveFile);
            }
            saveFile.Save();
            Debug.Log("Game saved");
        }
        
        public void LoadGame() {
            if (!HasSaveData()) {
                Debug.Log("Tried loading but no data");
                saveFile.AddOrUpdateData(InitialSaveDataID, 0);
                saveFile.Save();
                return;
            }
            
            foreach (ISavable savableObject in savableObjects) {
                savableObject.LoadSaveData(saveFile);
            }
            Debug.Log("Game loaded");
        }

        private List<ISavable> FindAllSavableObjects() {
            IEnumerable<ISavable> objects = FindObjectsOfType<MonoBehaviour>(true)
                .OfType<ISavable>();
            return new List<ISavable>(objects);
        }

        public bool HasSaveData() {
            return saveFile.HasData(InitialSaveDataID);
        }

        private void OnApplicationQuit() {
            SaveGame();
        }
    }
}