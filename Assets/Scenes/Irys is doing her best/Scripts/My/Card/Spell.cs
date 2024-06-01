using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Card {
    public class Spell : Card {
        public override void Initialize(CardData cardData) {
            base.Initialize(cardData);
            if (cardData is not SpellData spellData) {
                Debug.LogError("Invalid data type passed to Spell.Initialize");
            }
            else {
                cardDisplay.SetCardData(spellData);
            }
        }
    }
}