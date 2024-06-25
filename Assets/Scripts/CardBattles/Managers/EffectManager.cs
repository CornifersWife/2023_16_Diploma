using System.Collections;
using System.Collections.Generic;
using CardBattles.Enums;
using CardBattles.Interfaces;
using UnityEngine;

namespace CardBattles.Managers {
    public static class EffectManager {
        private static Dictionary<EffectName, EffectAnimationManager.EffectAnimationDelegate> Animation =>
            EffectAnimationManager.Animation;

        public delegate IEnumerator EffectDelegate(List<GameObject> targets, int value);

        public static Dictionary<EffectName, EffectDelegate>
            effectDictionary =
                new() {
                    { EffectName.Heal, Heal },
                    { EffectName.DealDamage, DealDamage },
                    { EffectName.ChangeAttack, ChangeAttack }
                };


        private static IEnumerator Heal(List<GameObject> targets, int heal) {
            foreach (var target in targets) {
                if (target.TryGetComponent(typeof(IDamageable), out var component)) {
                    ((IDamageable)component).Heal(heal);
                    yield return Animation[EffectName.Heal](component);
                }
            }
        }


        private static IEnumerator DealDamage(List<GameObject> targets, int damage) {
            foreach (var target in targets) {
                if (target.TryGetComponent(typeof(IDamageable), out var component)) {
                    ((IDamageable)component).TakeDamage(damage);
                    yield return Animation[EffectName.DealDamage](component);
                }
            }
        }

        private static IEnumerator ChangeAttack(List<GameObject> targets, int value) {
            throw new System.NotImplementedException();
        }
    }
}