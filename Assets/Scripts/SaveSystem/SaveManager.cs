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

        public static SaveManager Instance;

        private void OnEnable() {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable() {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
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
            saveFile = new SaveFile(saveFileSetup.saveFileData);
        }

        public void SaveGame() {
            if (!HasSaveFile()) {
                return;
            }
            
            foreach (ISavable savableObject in savableObjects) {
                savableObject.PopulateSaveData(saveFile);
            }
            saveFile.Save();
        }
        
        public void LoadGame() {
            if (!HasSaveFile()) {
                return;
            }
            
            foreach (ISavable savableObject in savableObjects) {
                savableObject.LoadSaveData(saveFile);
            }
        }

        private List<ISavable> FindAllSavableObjects() {
            IEnumerable<ISavable> objects = FindObjectsOfType<MonoBehaviour>(true)
                .OfType<ISavable>();
            return new List<ISavable>(objects);
        }

        public bool HasSaveFile() {
            return saveFile != null;
        }
    }
}