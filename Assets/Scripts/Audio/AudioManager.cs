using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {
    
    public static AudioManager Instance;

    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;

    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip villageMusic;
    [SerializeField] private AudioClip villageAmbience;
    [SerializeField] private AudioClip cardBattleMusic;
    
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        else {
            Instance = this;
        }
        DontDestroyOnLoad(this);
    }

    private void Update() {
        SceneManager.activeSceneChanged += ChangeMusic;
    }

    private void ChangeMusic(Scene scene, Scene scene1) {
        if (scene1.name == "Main Menu") {
            musicAudioSource.clip = menuMusic;
            sfxAudioSource.mute = true;
        }
        
        else if (scene1.name is "beta-release" or "beta-release-2") {
            sfxAudioSource.mute = false;
            musicAudioSource.clip = villageMusic;
            sfxAudioSource.clip = villageAmbience;
            musicAudioSource.Play();
            sfxAudioSource.Play();
        }
        
        else if (scene1.name == "Irys playspace") {
            musicAudioSource.clip = cardBattleMusic;
            sfxAudioSource.mute = true;
            musicAudioSource.Play();
            sfxAudioSource.Play();
        }
    }
}