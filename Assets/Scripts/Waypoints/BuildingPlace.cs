using System.Collections.Generic;
using UnityEngine;

public class BuildingPlace : MonoBehaviour {
    public bool IsComplete => _currentEXP >= _necessaryEXP;

    [SerializeField] private int _necessaryEXP;
    private int _currentEXP;

    private List<Worker> _workers = new List<Worker>();

    [SerializeField] private GameObject BuildingPrefab;
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
            Instantiate(BuildingPrefab, transform.position, Quaternion.identity);
        }
    }
}
