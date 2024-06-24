using UnityEngine;

namespace CardBattles.ForEditor {
    public abstract class ForEditorComponent : MonoBehaviour
    {
        private void Awake() {
            if (!Application.isEditor)
                enabled = false;
        }
    }
}
