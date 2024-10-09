
using UnityEngine;

namespace Editor.CardBattles {
    public static class ScriptableObjectUtility {
        public static T Clone<T>(T original) where T : ScriptableObject {
            T clone = Object.Instantiate(original);
            clone.name = original.name; // Keep the name to avoid "(Clone)" suffix
            return clone;
        }
    }
}
