using System.Collections;
using UnityEngine;
using Toblerone.Toolbox;

public class GameSessionManager : MonoBehaviour {
    [Header("Config")]
    [SerializeField] private float showButtonsDelay;
    [Header("References")]
    [SerializeField] private GameObject _gameVictoryScreen;
    [SerializeField] private GameObject _victoryButtons;
    [SerializeField] private GameObject _gameDefeatScreen;
    [SerializeField] private GameObject _defeatButtons;
    [Header("Timer")]
    [SerializeField, Range(60, 540), Tooltip("time in seconds")] private int timeLimit = 60;
    [SerializeField] private FloatVariable timerVariable;
    [Header("External References")]
    [SerializeField] private PlayerVariable _playerRef;
    [SerializeField] private GenericEvent<bool> endGameEvent;
    [SerializeField] private RuntimeSet<BuildingPlace> buildingPlaces;
    private bool gameEnded;

    private void Awake() {
        Time.timeScale = 1.0f;
        gameEnded = false;
    }

    private void Start() {
        timerVariable.Value = timeLimit;
    }

    private void Update() {
        if (gameEnded)
            return;
        timerVariable.Value -= Time.deltaTime;
        if (WinConditionTriggered()) {
            Time.timeScale = 0;
            StartCoroutine(ShowEndScreen(true));
        } else if (DefeatConditionTriggered()) {
            Time.timeScale = 0;
            StartCoroutine(ShowEndScreen(false));
        }
    }

    private IEnumerator ShowEndScreen(bool victory) {
        gameEnded = true;
        endGameEvent.Raise(victory);
        GameObject endScreen = victory ? _gameVictoryScreen : _gameDefeatScreen;
        endScreen.SetActive(true);
        yield return new WaitForSecondsRealtime(showButtonsDelay);
        GameObject buttonsObj = victory ? _victoryButtons : _defeatButtons;
        buttonsObj.SetActive(true);
        CustomInputs customInputs = new CustomInputs();
        customInputs.Enable();
    }

    private bool WinConditionTriggered() {
        foreach (BuildingPlace buildingPlace in buildingPlaces) {
            if (!buildingPlace.IsComplete) {
                return false;
            }
        }
        return true;
    }

    private bool DefeatConditionTriggered() {
        return _playerRef.Value == null || timerVariable.Value < 0;
    }
}
