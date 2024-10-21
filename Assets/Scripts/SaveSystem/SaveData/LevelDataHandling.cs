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

        private const string ItemSaveID = "Collectible item ";
        private const string ObstacleSaveID = "Obstacle ";

        private void Awake() { 
            IEnumerable<Obstacle> objects = FindObjectsOfType<MonoBehaviour>(true)
                .OfType<Obstacle>();
            allObstacles = new List<Obstacle>(objects);
        }
        
        public void PopulateSaveData(SaveFile saveFile) {
            int i = 0;
            foreach (Obstacle obstacle in allObstacles) {
                saveFile.AddOrUpdateData(ObstacleSaveID + i, obstacle.IsObstacle());
                i++;
            }
        }

        public void LoadSaveData(SaveFile saveFile) {
            if (!saveFile.HasData(ObstacleSaveID + 0))
                return;
            
            int i = 0;
            foreach (Obstacle obstacle in allObstacles) {
                bool isObstacle = saveFile.GetData<bool>(ObstacleSaveID + i);
                obstacle.SetObstacle(isObstacle);
                i++;
            }
        }
    }
}