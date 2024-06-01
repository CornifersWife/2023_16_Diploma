using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Card {
    public class CardManager : MonoBehaviour {
        public GameObject minionPrefab;
        public GameObject spellPrefab;

        public Card CreateCard(CardData cardData) {
            GameObject cardObject = null;
            Card cardComponent = null;

            if (cardData is MinionData) {
                cardObject = Instantiate(minionPrefab);
                cardComponent = cardObject.GetComponent<Minion>();
            } else if (cardData is SpellData) {
                cardObject = Instantiate(spellPrefab);
                cardComponent = cardObject.GetComponent<Spell>();
            }

            if (cardComponent is not null) {
                cardComponent.Initialize(cardData);
            } else {
                Debug.LogError("Failed to create cardOld.");
            }

            return cardComponent;
        }
    }
}
