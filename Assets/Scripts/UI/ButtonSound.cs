using UnityEngine;

public class ButtonSound : MonoBehaviour {
    [SerializeField] private AudioClip buttonSFX;
    public void PlaySFX() {
        SoundPlayer.Instance?.PlaySFX(buttonSFX);
    }
}
