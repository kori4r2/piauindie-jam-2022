using UnityEngine;

public class GameSessionManager : MonoBehaviour {
    private BuildingPlace[] _buildingPlaces;

    [SerializeField] private GameObject _gameVictoryScreen;

    private void Awake() {
        _buildingPlaces = FindObjectsOfType<BuildingPlace>();
    }

    private void Update() {
        if(WinConditionTriggered() == true) {
            _gameVictoryScreen.SetActive(true);
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
}
