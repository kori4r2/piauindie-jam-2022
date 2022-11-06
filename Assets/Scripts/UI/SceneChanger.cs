using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {
    [SerializeField] private ScenePicker sceneToChange;
    [SerializeField] private AudioClip newBGM;

    public void ChangeScene() {
        if (newBGM != null)
            SceneManager.sceneLoaded += PlayNewBGM;
        SceneManager.LoadScene(sceneToChange.Name);
    }

    private void PlayNewBGM(Scene scene, LoadSceneMode sceneMode) {
        SoundPlayer.Instance?.PlayBGM(newBGM);
        SceneManager.sceneLoaded -= PlayNewBGM;
    }

    public void ReloadScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#elif UNITY_WEBGL
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
#else
        Application.Quit();
#endif
    }
}
