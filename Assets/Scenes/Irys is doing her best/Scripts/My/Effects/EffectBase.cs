using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My {
    //[CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public abstract class EffectBase : ScriptableObject, IEffect {
        public abstract void ApplyEffect(ICollection<GameObject> targets);
    }
}