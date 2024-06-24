using System.Collections.Generic;
using CardBattles.Enums;
using CardBattles.Interfaces;
using UnityEngine;
using Component = System.ComponentModel.Component;

namespace CardBattles.Managers {
    public static class EffectManager {
        
        
        public delegate void EffectDelegate(List<GameObject> targets, int value);

        public static Dictionary<EffectName, EffectDelegate> 
            effectDictionary =
            new Dictionary<EffectName, EffectDelegate> {
                { EffectName.Heal , Heal },
                { EffectName.DealDamage, DealDamage },
                { EffectName.ChangeAttack, ChangeAttack}
            };

        private static void Heal(List<GameObject> targets, int heal) {
            foreach (var target in targets) {
                if(target.TryGetComponent(typeof(IDamageable), out var component))
                    ((IDamageable)component).Heal(heal);
            }
        }

        private static void DealDamage(List<GameObject> targets, int damage) {
            foreach (var target in targets) {
                if(target.TryGetComponent(typeof(IDamageable), out var component))
                    ((IDamageable)component).TakeDamage(damage);
            }
        }

        private static void ChangeAttack(List<GameObject> targets, int value) {
            throw new System.NotImplementedException();
        }
    }
}