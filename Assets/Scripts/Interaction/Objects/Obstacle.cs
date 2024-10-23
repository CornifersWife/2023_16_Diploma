using System;
using UnityEngine;

namespace Interaction.Objects {
    public class Obstacle: MonoBehaviour {
        [SerializeField] private string obstacleID;
        [ContextMenu("Generate guid for id")]
        private void GenerateGuid() {
            obstacleID = Guid.NewGuid().ToString();
        }

        public void SetObstacle(bool value) {
            gameObject.SetActive(value);
        }

        public bool IsObstacle() {
            return gameObject.activeSelf;
        }

        public string GetID() {
            return obstacleID;
        }
    }
}