using System.Collections.Generic;
using System.Linq;
using Esper.ESave;
using Interaction.Objects;
using UnityEngine;

namespace SaveSystem.SaveData {
    public class LevelDataHandling:MonoBehaviour, ISavable {
        //TODO handle saving collectible items in the world
        private List<GameObject> allItems;
        private List<Obstacle> allObstacles;

        private const string ItemSaveData = "Collectible item data";
        private const string ObstacleSaveData = "Obstacle data";

        private void Awake() { 
            IEnumerable<Obstacle> objects = FindObjectsOfType<MonoBehaviour>(true)
                .OfType<Obstacle>();
            allObstacles = new List<Obstacle>(objects);
        }
        
        public void PopulateSaveData(SaveFile saveFile) {
            saveFile.AddOrUpdateData(ObstacleSaveData, ObstacleSaveData);
            foreach (Obstacle obstacle in allObstacles) {
                string id = obstacle.GetID();
                if(saveFile.HasData(id))
                    saveFile.DeleteData(id);
                saveFile.AddOrUpdateData(id, obstacle.IsObstacle());
            }
        }

        public void LoadSaveData(SaveFile saveFile) {
            if (!saveFile.HasData(ObstacleSaveData))
                return;
            
            foreach (Obstacle obstacle in allObstacles) {
                bool isObstacle = saveFile.GetData<bool>(obstacle.GetID());
                obstacle.SetObstacle(isObstacle);
            }
        }
    }
}