using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Card {
    public class Spell : Card {
        public override void Initialize(CardData cardData) {
            base.Initialize(cardData);
            if (cardData is not SpellData) {
                Debug.LogError("Invalid data type passed to Spell.Initialize");
            }
        }
    }
}