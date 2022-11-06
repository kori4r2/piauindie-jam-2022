using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {
    [SerializeField] private ScenePicker sceneToChange;

    public void ChangeScene() {
        SceneManager.LoadScene(sceneToChange.Name);
    }

    public void ReloadScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
