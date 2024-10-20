using System.Collections;
using System.Collections.Generic;
using CardBattles.Enums;
using UnityEngine;

namespace CardBattles.Managers {
    public static class EffectAnimationManager {
        public delegate IEnumerator EffectAnimationDelegate(Component target);

        public static Dictionary<EffectName, EffectAnimationDelegate> 
            Animation =
                new() {
                    { EffectName.Heal , HealAnimation},
                    { EffectName.DealDamage, DealDamageAnimation },
                    { EffectName.ChangeAttack, ChangeAttackAnimation}
                };
        
        private static IEnumerator HealAnimation(Component target) {
            yield return null;
        }
        
        private static IEnumerator DealDamageAnimation(Component target) {
            yield return null;
        }

        private static IEnumerator ChangeAttackAnimation(Component target) {
            yield return null;
        }

    }
}