using Scenes.Irys_is_doing_her_best.Scripts.My.Card;
using Scenes.Irys_is_doing_her_best.Scripts.My.CardDatas;
using UnityEditor;
using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Singletons {
    public class Testing : MonoBehaviour {
        public static Testing Instance { get; private set; }

        private void Awake() {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject); // Optional: makes the manager persist across scenes
            }
            else {
                Destroy(gameObject);
            }
        }

        [field: Header("CardManager")] [SerializeField]
        public CardData CardData;

        public void TestCreateCard() {
            var output = CardManager.Instance.CreateCard(CardData,gameObject);
            Debug.Log(output);
        }
    }
}