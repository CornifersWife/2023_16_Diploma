using CardBattles.CardScripts;
using CardBattles.CardScripts.CardDatas;
using CardBattles.Character;
using CardBattles.Interfaces.InterfaceObjects;
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

        public Card CreateCard(CardData cardData, PlayerEnemyMonoBehaviour parentComponent) {
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

            cardComponent.Initialize(cardData,parentComponent.IsPlayers); 
            cardComponent.transform.SetParent(parentComponent.transform);
            cardComponent.transform.localPosition = Vector3.zero;

            return cardComponent;
        }

        public void PlayACard(CharacterManager character, Card card, CharacterManager.ITarget target) {
            switch (card)
            {
                case Minion minionCard:
                    PlayAMinion(character,minionCard, target);
                    break;

                case Spell spellCard:
                    PlayASpell(character,spellCard, target);
                    break;

                default:
                    Debug.LogError("Unknown card type");
                    break;
            }
        }

        private void PlayAMinion(CharacterManager character, Minion minion, CharacterManager.ITarget target) {
            switch (target)
            {
                case CardSpot cardSpot:
                    break;

                default:
                    Debug.LogError("Unknown target type");
                    break;
            }
        }

        private void PlayASpell(CharacterManager character, Spell spell, CharacterManager.ITarget target) {
            throw new System.NotImplementedException();
        }
    }
}