using Scenes.Irys_is_doing_her_best.Scripts.My.Board;
using Scenes.Irys_is_doing_her_best.Scripts.My.CardDatas;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Singletons {
    public class Testing : MonoBehaviour {
        public static Testing Instance { get; private set; }

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            else {
                Destroy(gameObject);
            }
        }

        [SerializeField]
        private Button changeTurnButton;

        private void Update() {
            changeTurnButton.interactable = TurnManager.Instance.isPlayersTurn;
        }
        



    }
}