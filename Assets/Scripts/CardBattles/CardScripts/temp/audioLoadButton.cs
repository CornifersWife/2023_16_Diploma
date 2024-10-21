using NaughtyAttributes;
using UnityEngine;

namespace CardBattles.CardScripts.temp {
    public class audioLoadButton : MonoBehaviour {
        [SerializeField] public AudioCollection ad;

        [Button]
        private void DoLoad() {
            ad.DoLoad();
        }

        [Button]
        private void OverwriteJson() {
            ad.OverwriteJson();
        }

        [Button]
        private void SaveJsonToFile() {
            ad.SaveJsonToFile();
        }

        [Button]
        private void CreateJsonPaths() {
            ad.CreateJsonPaths();
        }
    }
}