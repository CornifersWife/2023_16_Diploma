using System;
using System.Collections.Generic;
using Scenes.Irys_is_doing_her_best.Scripts.My.Enums;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scenes.Irys_is_doing_her_best.Scripts.My {
    [Serializable]
    public class EffectTargetPair {
        [FormerlySerializedAs("Effect")] public EffectBase effect;
        [FormerlySerializedAs("TargetType")] public TargetType targetType;

        public void ApplyEffect() {
            throw new NotImplementedException();

        }
    }
}