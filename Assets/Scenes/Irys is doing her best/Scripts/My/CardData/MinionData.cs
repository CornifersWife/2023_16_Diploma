using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My {
    [CreateAssetMenu(fileName = "New Minion Data", menuName = "Card Data/Minion")]
    public class MinionData : CardData {
        [SerializeField]
        private int attack;
        public int Attack {
            get => attack;
            set => attack = value;
        }

        [SerializeField]
        private int maxHealth;
        public int MaxHealth {
            get => maxHealth;
            set => maxHealth = value;
        }

        [SerializeField]
        private List<EffectTargetPair> onDeathEffects = new List<EffectTargetPair>();
        public List<EffectTargetPair> OnDeathEffects {
            get => onDeathEffects;
            set => onDeathEffects = value;
        }

        public void TriggerOnDeathEffects() {
            foreach (var effect in OnDeathEffects) {
                effect.ApplyEffect();
            }
        }
    }
}