using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My {
    public class HealEffect : ChangeValueByEffect {
        public override void ApplyEffect(ICollection<GameObject> targets) {
            var trueTargets =targets
                .Where(t => t.GetComponent<IDamageable>() is not null)
                .Cast<IDamageable>();
            foreach (var target in trueTargets) {
                target.Heal(amount);
            }
        }
    }
}