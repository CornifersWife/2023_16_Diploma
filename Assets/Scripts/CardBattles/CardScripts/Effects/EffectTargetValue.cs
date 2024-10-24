using System;
using CardBattles.Enums;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace CardBattles.CardScripts.Effects {
    [Serializable]
    public class EffectTargetValue {
        [SerializeField,AllowNesting,Required]
        public EffectName effectName;
        [SerializeField,AllowNesting,Required]
        public TargetType targetType;
        [SerializeField,AllowNesting,Required]
        public int value;

        private EffectTargetValue(EffectName effectName, TargetType targetType, int value) {
            this.effectName = effectName;
            this.targetType = targetType;
            this.value = value;
        }
        
        public EffectTargetValue Copy() {
            return new EffectTargetValue(effectName, targetType, value);
        }
    }
}