using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Structure {
    [Serializable]
    public abstract class EffectBase : MonoBehaviour, IEffect {
        public abstract void ApplyEffect(ICollection<GameObject> targets);
    }
}