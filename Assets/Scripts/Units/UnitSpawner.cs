using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour {
    [Header("Config")]
    [SerializeField] private List<AIUnit> unitsToSpawn = new List<AIUnit>();
    [SerializeField] private List<Vector2> spawnPositions = new List<Vector2>();
    [SerializeField, Range(1, 20)] private int maxUnits = 1;
    [SerializeField, Range(1, 20f)] private float respawnCooldown = 2f;
    [SerializeField, Range(0, 20f)] private float firstSpawnDelay = 2f;
    [Header("References")]
    [SerializeField] private AIUnitRuntimeSet unitsRuntimeSet;
    [Header("Debug")]
    [SerializeField] private bool _drawGizmos;
    [SerializeField] private Color _gizmosColor;

    private int currentUnitIndex = 0;
    private int currentPositionIndex = 0;
    private float currentTimer;

    private void Awake() {
        currentTimer = firstSpawnDelay;
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

        Vector2 position = spawnPositions[currentPositionIndex++];
        currentPositionIndex %= spawnPositions.Count;
        return new Vector3(position.x, position.y, 0);
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
