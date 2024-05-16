using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound {
    public string soundName;

    public AudioClip clip;
    public AudioMixerGroup mixer;
    
    public bool loop = false;
    
    [HideInInspector]
    public AudioSource source;
}