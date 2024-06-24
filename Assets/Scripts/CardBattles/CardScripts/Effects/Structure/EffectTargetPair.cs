using System;
using CardBattles.Enums;
using UnityEngine.Serialization;

namespace CardBattles.CardScripts.Effects.Structure {
    [Serializable]
    public class EffectTargetPair {
        [FormerlySerializedAs("Effect")] public EffectBase effect;
        [FormerlySerializedAs("TargetType")] public TargetType targetType;

        public void ApplyEffect() {
            throw new NotImplementedException();

        }
    }
}