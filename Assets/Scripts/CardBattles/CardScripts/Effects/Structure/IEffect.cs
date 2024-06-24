using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Structure {
    public interface IEffect {
        void ApplyEffect(ICollection<GameObject> targets);
    }
}