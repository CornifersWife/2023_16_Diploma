using Scenes.Irys_is_doing_her_best.Scripts.My.CardDatas;
using Scenes.Irys_is_doing_her_best.Scripts.My.Cards;
using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Singletons {
    public class CardManager : MonoBehaviour {

        public static CardManager Instance;

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
        
        public Cards.Card CreateCard(CardData cardData, Transform parentTransform) {
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
            cardComponent.transform.SetParent(parentTransform);
            cardComponent.transform.localPosition = Vector3.zero;

            return cardComponent;
        }
        
    }
}