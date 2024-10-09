using UnityEngine;

namespace Audio {
    [CreateAssetMenu(fileName = "DialogueAudioConfig", menuName = "Audio/DialogueAudioConfig")]
    public class DialogueAudioConfig : ScriptableObject {
        public string id;
        public AudioClip[] dialogueAudios;
        [Range(1, 5)]
        public int frequencyLevel = 2;
        [Range(-3, 3)]
        public float minPitch = 0.5f;
        [Range(-3, 3)]
        public float maxPitch = 3f;
        public bool stopAudioSource;
    }
}