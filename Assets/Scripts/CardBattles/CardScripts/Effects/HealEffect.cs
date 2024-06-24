using System.Collections.Generic;
using System.Linq;
using CardBattles.CardScripts.Effects.Structure;
using CardBattles.Interfaces;
using UnityEngine;

namespace CardBattles.CardScripts.Effects {
    [CreateAssetMenu(fileName = "Heal Effect", menuName = "Effects/Heal Effect")]
    public class HealEffect : ChangeValueByEffect {
        public override void ApplyEffect(ICollection<GameObject> targets) {
            var trueTargets = targets
                .Where(t => t.GetComponent<IDamageable>() is not null)
                .Cast<IDamageable>();
            foreach (var target in trueTargets) {
                target.Heal(amount);
            }
        }
    }
}