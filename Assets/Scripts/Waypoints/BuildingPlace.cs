using System.Collections.Generic;
using UnityEngine;
using Toblerone.Toolbox;

public class BuildingPlace : MonoBehaviour {
    public bool IsComplete => _currentEXP >= _necessaryEXP;

    [SerializeField, Range(0, 20)] private int pathID = 0;
    public int PathID => pathID;
    [SerializeField] private int _necessaryEXP;
    private int _currentEXP;

    private List<Worker> _workers = new List<Worker>();

    [SerializeField] private GameObject BuildingPrefab;
    [SerializeField] private IntEventSO finishedBuildingEvent;
    [SerializeField] private AudioClip finishedBuildingSFX;
    [SerializeField] private BuildingPlaceRuntimeSet runtimeSet;

    private void OnEnable() {
        runtimeSet.AddElement(this);
    }

    private void OnDisable() {
        runtimeSet.RemoveElement(this);
    }

    public void OnWorkerArrived(Worker worker) {
        _workers.Add(worker);
        ChangeEXP(worker.GrantedEXP);
        if (_workers.Count > 1) {
            MoveNewWorker();
        }
    }

    private void MoveNewWorker() {
        float angle = CalculateAngleOffset();
        Vector3 newPosition = CalculateNewPosition(angle);
        _workers[_workers.Count - 1].transform.position = newPosition;
    }

    private float CalculateAngleOffset() {
        Worker lastWorker = _workers[_workers.Count - 2];
        Worker newWorker = _workers[_workers.Count - 1];
        float progressOffset = (lastWorker.GrantedEXP + newWorker.GrantedEXP) / (2f * _necessaryEXP);
        return progressOffset * 360f;
    }

    private Vector3 CalculateNewPosition(float angle) {
        Vector3 fromVector = _workers[_workers.Count - 2].transform.position - transform.position;
        Vector3 toVector = Quaternion.AngleAxis(angle, Vector3.back) * fromVector;
        return transform.position + toVector;
    }

    public void OnWorkerDied(Worker worker) {
        _workers.Remove(worker);
        _currentEXP -= worker.GrantedEXP;
    }

    private void ChangeEXP(int amount) {
        _currentEXP += amount;

        if (IsComplete == true) {
            for (int i = 0; i < _workers.Count; i++) {
                _workers[i].GoHome();
            }
            finishedBuildingEvent.Raise(PathID);
            SoundPlayer.Instance?.PlaySFX(finishedBuildingSFX);
            Instantiate(BuildingPrefab, transform.position, Quaternion.identity);
        }
    }
}
