using System;
using CardBattles.Enums;
using NaughtyAttributes;
using UnityEngine;

namespace CardBattles.CardScripts.Effects {
    [Serializable]
    public class EffectTargetValue {
        [SerializeField,AllowNesting]
        public EffectName effect;
        [SerializeField,AllowNesting]
        public TargetType targetType;
        [SerializeField,AllowNesting]
        public int value;
    }
}