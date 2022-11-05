using UnityEngine;

public class Waypoint : MonoBehaviour {
    private Waypoint _nextWaypoint;
    private BasePoint _basePoint;

    public void SetWaypoint(Waypoint nextWaypoint) {
        _nextWaypoint = nextWaypoint;
        // if the next waypoint is also a base point, we cache it already
        // so when the character reaches it, we can ask if the base point is already complete
        // which means that the waypoint should be skipped
        _basePoint = nextWaypoint.GetComponent<BasePoint>();
    }
}
