using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My {
    public interface IEffect {
        void ApplyEffect(ICollection<GameObject> targets);
    }
}