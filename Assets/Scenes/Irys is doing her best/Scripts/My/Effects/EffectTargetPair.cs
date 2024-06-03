using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My {
    [Serializable]
    public class EffectTargetPair {
        public EffectBase Effect { get; set; }
        public TargetType TargetType { get; set; }

        public void ApplyEffect() {
            throw new NotImplementedException();
            
            /*
            var targets = SomeManager.GetTargets(TargetType);
            effect.ApplyEffect(targets);
            */
            
        }
    }
}