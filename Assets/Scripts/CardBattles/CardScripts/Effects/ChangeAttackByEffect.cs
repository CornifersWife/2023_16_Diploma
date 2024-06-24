using System.Collections.Generic;
using CardBattles.CardScripts.Effects.Structure;
using UnityEngine;

namespace CardBattles.CardScripts.Effects {
    [CreateAssetMenu(fileName = "Change Attack By Effect",menuName = "Effects/Change Attack By Effect")]
    public class ChangeAttackByEffect : ChangeValueByEffect {
        public override void ApplyEffect(ICollection<GameObject> targets) {
            throw new System.NotImplementedException();
        }
    }
}