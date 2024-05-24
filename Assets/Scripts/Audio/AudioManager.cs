using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {
    
    public static AudioManager Instance;
    public Sound[] sounds;
    
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        else {
            Instance = this;
        }
        DontDestroyOnLoad(this);

        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.mixer;
        }
    }

    private void Start() {
        //PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic() {
        if (SceneManager.GetActiveScene().name == "Level") {
            Play("Level Music");
        }
        else if (SceneManager.GetActiveScene().name == "Main Menu") {
            Play("Main Menu Music");
            Play("Magic Click");
        }
    }

    public void Play(string sound) {
        Sound s = Array.Find(sounds, item => item.soundName == sound);
        s.source.Play();
    }
    
    public void Stop(string sound) {
        Sound s = Array.Find(sounds, item => item.soundName == sound);
        s.source.Stop();
    }
}