using System;
using Scenes.Irys_is_doing_her_best.Scripts.My.Enums;
using UnityEngine.Serialization;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Structure {
    [Serializable]
    public class EffectTargetPair {
        [FormerlySerializedAs("Effect")] public EffectBase effect;
        [FormerlySerializedAs("TargetType")] public TargetType targetType;

        public void ApplyEffect() {
            throw new NotImplementedException();

        }
    }
}