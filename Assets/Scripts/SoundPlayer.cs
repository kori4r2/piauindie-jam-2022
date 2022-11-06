using UnityEngine;

public class SoundPlayer : MonoBehaviour {
    public static SoundPlayer Instance { get; private set; } = null;
    [SerializeField] private AudioSource audioSource;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
        if (Instance != null && Instance != this) {
            Destroy(this);
            return;
        }
        Instance = this;
        audioSource.loop = true;
        audioSource.playOnAwake = true;
        if (audioSource.clip != null)
            audioSource.Play();
    }

    private void OnDestroy() {
        if (Instance == this)
            Instance = null;
    }

    public void PlayBGM(AudioClip bgm) {
        if (bgm == null || bgm == audioSource.clip) {
            return;
        }
        audioSource.Stop();
        audioSource.clip = bgm;
        audioSource.Play();
    }

    public void StopBGM() {
        audioSource.Stop();
    }

    public void PlaySFX(AudioClip sfx) {
        if (sfx != null)
            audioSource.PlayOneShot(sfx);
    }
}
