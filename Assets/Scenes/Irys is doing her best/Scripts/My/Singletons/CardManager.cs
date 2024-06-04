using Scenes.Irys_is_doing_her_best.Scripts.My.CardDatas;
using Scenes.Irys_is_doing_her_best.Scripts.My.Cards;
using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Singletons {
    public class CardManager : MonoBehaviour {
        
        public static CardManager Instance { get; private set; }

        public GameObject minionPrefab;
        public GameObject spellPrefab;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject); // Optional: makes the manager persist across scenes
            } else {
                Destroy(gameObject);
            }
        }
        
        public Cards.Card CreateCard(CardData cardData, GameObject parentGameObject) {
            var parent = parentGameObject.transform.parent;
            GameObject cardObject;
            Cards.Card cardComponent = null;

            if (cardData is MinionData) {
                cardObject = Instantiate(minionPrefab);
                cardComponent = cardObject.GetComponent<Minion>();
                
            } else if (cardData is SpellData) {
                cardObject = Instantiate(spellPrefab);
                cardComponent = cardObject.GetComponent<Spell>();
            }
            if (cardComponent is null) {
                Debug.LogError("Failed to create card.");
                return null;
            }

            cardComponent.Initialize(cardData);
            cardComponent.transform.parent = parent;
            

            return cardComponent;
        }
        
    }
}