using System.Collections.Generic;
using UnityEngine;
using Toblerone.Toolbox;

public class UnitSpawner : MonoBehaviour {
    [Header("Config")]
    [SerializeField] private List<AIUnit> unitsToSpawn = new List<AIUnit>();
    [SerializeField] private List<Vector2> spawnPositions = new List<Vector2>();
    [SerializeField, Range(1, 20)] private int maxUnits = 1;
    [SerializeField, Range(1, 20f)] private float respawnCooldown = 2f;
    [SerializeField, Range(0, 20f)] private float firstSpawnDelay = 2f;
    [Header("References")]
    [SerializeField] private AIUnitRuntimeSet unitsRuntimeSet;
    [SerializeField] private BuildingPlaceRuntimeSet buildingPlaces;
    [SerializeField] private IntEventSO buildingCompleted;
    private GenericEventListener<int> buildingCompletedListener;
    [Header("Debug")]
    [SerializeField] private bool _drawGizmos;
    [SerializeField] private Color _gizmosColor;

    private int currentUnitIndex = 0;
    private int currentPositionIndex = -1;
    private float currentTimer;
    private bool[] isSpawnPointActive;

    private void Awake() {
        currentTimer = firstSpawnDelay;
        isSpawnPointActive = new bool[spawnPositions.Count];
        SetSpawnPointsStatus(true);
        buildingCompletedListener = new GenericEventListener<int>(buildingCompleted, CheckPathsCompleted);
    }

    private void SetSpawnPointsStatus(bool newStatus) {
        for (int index = 0; index < isSpawnPointActive.Length; index++) {
            isSpawnPointActive[index] = newStatus;
        }
    }

    private void CheckPathsCompleted(int pathID) {
        SetSpawnPointsStatus(false);
        foreach (BuildingPlace buildingPlace in buildingPlaces) {
            if (buildingPlace.PathID < 0 || buildingPlace.PathID >= spawnPositions.Count)
                continue;
            isSpawnPointActive[buildingPlace.PathID] |= !buildingPlace.IsComplete;
        }
    }

    private void OnEnable() {
        buildingCompletedListener.StartListeningEvent();
    }

    private void OnDisable() {
        buildingCompletedListener.StopListeningEvent();
    }

    private void Update() {
        if (unitsToSpawn.Count <= 0 || unitsRuntimeSet.Count >= maxUnits)
            return;
        if (currentTimer > 0) {
            currentTimer -= Time.deltaTime;
            return;
        }
        SpawnNewUnit();
    }

    private void SpawnNewUnit() {
        currentTimer = respawnCooldown;
        AIUnit nextUnit = GetNextUnit();
        Vector3 nextPosition = GetNextPosition();
        Instantiate(nextUnit, nextPosition, Quaternion.identity, transform);
    }

    private AIUnit GetNextUnit() {
        AIUnit selectedUnit = unitsToSpawn[currentUnitIndex++];
        currentUnitIndex %= unitsToSpawn.Count;
        return selectedUnit;
    }

    private Vector3 GetNextPosition() {
        if (spawnPositions.Count <= 0)
            return Vector2.zero;

        CalculateNextValidIndex();
        Vector2 position = spawnPositions[currentPositionIndex];
        return new Vector3(position.x, position.y, 0);
    }

    private void CalculateNextValidIndex() {
        int count = 0;
        do {
            currentPositionIndex++;
            currentPositionIndex %= spawnPositions.Count;
            count++;
        } while (!isSpawnPointActive[currentPositionIndex] && count < spawnPositions.Count);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos() {
        Gizmos.color = _gizmosColor;
        if (_drawGizmos == false) {
            return;
        }

        foreach (Vector2 position in spawnPositions) {
            Gizmos.DrawWireSphere(position, .5f);
        }
    }
#endif
}
