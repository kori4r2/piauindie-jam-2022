using UnityEngine;

public class Waypoint : MonoBehaviour {
    private Waypoint _nextWaypoint;
    public Waypoint NextWaypoint => _nextWaypoint;

    private BuildingPlace _buildingPlace;
    public BuildingPlace BuildingPlace => _buildingPlace;
    [SerializeField] private WaypointRuntimeSet runtimeSet;

    private void OnEnable() {
        runtimeSet.AddElement(this);
    }

    private void OnDisable() {
        runtimeSet.RemoveElement(this);
    }

    public void SetWaypoint(Waypoint nextWaypoint) {
        _nextWaypoint = nextWaypoint;
        // if the next waypoint is also a base point, we cache it already
        // so when the character reaches it, we can ask if the base point is already complete
        // which means that the waypoint should be skipped
        _buildingPlace = nextWaypoint.GetComponent<BuildingPlace>();
    }
}
