using CardBattles.CardScripts;
using CardBattles.CardScripts.CardDatas;
using UnityEngine;

namespace CardBattles.Managers {
    public class CardManager : MonoBehaviour {
        public static CardManager Instance;

        public GameObject minionPrefab;
        public GameObject spellPrefab;

        private void Awake() {
            if (Instance is null) {
                Instance = this;
                DontDestroyOnLoad(gameObject); 
            }
            else {
                Destroy(gameObject);
            }
        }

        public Card CreateCard(CardData cardData, Transform parentTransform, bool isPlayers) {
            GameObject cardObject;
            Card cardComponent = null;

            switch (cardData) {
                case MinionData:
                    cardObject = Instantiate(minionPrefab);
                    cardComponent = cardObject.GetComponent<Minion>();
                    break;
                case SpellData:
                    cardObject = Instantiate(spellPrefab);
                    cardComponent = cardObject.GetComponent<Spell>();
                    break;
            }

            if (cardComponent is null) {
                Debug.LogError("Failed to create card.");
                return null;
            }

            cardComponent.Initialize(cardData, isPlayers);
            cardComponent.transform.SetParent(parentTransform);
            cardComponent.transform.localPosition = Vector3.zero;

            return cardComponent;
        }
    }
}