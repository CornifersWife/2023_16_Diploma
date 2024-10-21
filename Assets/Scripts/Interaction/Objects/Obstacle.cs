using UnityEngine;

namespace Interaction.Objects {
    public class Obstacle: MonoBehaviour {

        public void SetObstacle(bool value) {
            gameObject.SetActive(value);
        }

        public bool IsObstacle() {
            return gameObject.activeSelf;
        }
    }
}