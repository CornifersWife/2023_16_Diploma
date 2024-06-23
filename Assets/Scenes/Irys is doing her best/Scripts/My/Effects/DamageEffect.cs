using System.Collections.Generic;
using System.Linq;
using Scenes.Irys_is_doing_her_best.Scripts.My.Interfaces;
using Scenes.Irys_is_doing_her_best.Scripts.My.Structure;
using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My {
    [CreateAssetMenu(fileName = "Damage Effect",menuName = "Effects/Damage Effect")]
    public class DamageEffect : ChangeValueByEffect {
        public override void ApplyEffect(ICollection<GameObject> targets) {
            var trueTargets =targets
                .Where(t => t.GetComponent<IDamageable>() is not null)
                .Cast<IDamageable>();
            foreach (var target in trueTargets) {
                target.TakeDamage(amount);
            }
        }
    }
}