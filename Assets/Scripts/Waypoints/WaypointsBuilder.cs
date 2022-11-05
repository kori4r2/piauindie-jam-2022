using UnityEngine;

public class WaypointsBuilder : MonoBehaviour {
    public void Awake() {
        // the last waypoint will never have a next one, of course
        for (int i = 0; i < transform.childCount - 1; i++) {
            var waypoint = transform.GetChild(i).GetComponent<Waypoint>();
            var nextWaypoint = transform.GetChild(i + 1).GetComponent<Waypoint>();
            waypoint.SetWaypoint(nextWaypoint);
        }
    }
}
