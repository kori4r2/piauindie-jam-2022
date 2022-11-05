using UnityEngine;

public class GameSessionManager : MonoBehaviour {
    private BuildingPlace[] _buildingPlaces;

    [SerializeField] private GameObject _gameVictoryScreen;
    [SerializeField] private GameObject _gameDefeatScreen;
    [SerializeField] private PlayerVariable _playerRef;

    private void Awake() {
        _buildingPlaces = FindObjectsOfType<BuildingPlace>();
    }

    private void Update() {
        if (WinConditionTriggered()) {
            Time.timeScale = 0;
            _gameVictoryScreen.SetActive(true);
        } else if (DefeatConditionTriggered()) {
            Time.timeScale = 0;
            _gameDefeatScreen.SetActive(true);
        }
    }

    private bool WinConditionTriggered() {
        for (int i = 0; i < _buildingPlaces.Length; i++) {
            if (_buildingPlaces[i].IsComplete == false) {
                return false;
            }
        }
        return true;
    }

    private bool DefeatConditionTriggered() {
        return _playerRef.Value == null;
    }
}
