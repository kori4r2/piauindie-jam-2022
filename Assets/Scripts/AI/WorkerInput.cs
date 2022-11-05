using UnityEngine;

public class WorkerInput : AIInput {
    private Waypoint _closestWaypoint;

    private void Awake() {
        _closestWaypoint = FindClosestWaypoint();
    }

    private Waypoint FindClosestWaypoint() {
        var smallestDistance = float.MinValue;
        var chosenWaypoint = default(Waypoint);

        var allWaypoints = FindObjectsOfType<Waypoint>();
        for (int i = 0; i < allWaypoints.Length; i++) {
            var distanceToWaypoint = Vector3.Distance(transform.position, allWaypoints[i].transform.position);
            if (distanceToWaypoint < smallestDistance) {
                smallestDistance = distanceToWaypoint;
                chosenWaypoint = allWaypoints[i];
            }
        }

        return chosenWaypoint;
    }

    private void Update() {
        MovementPerformed.Invoke(DirectionToWaypoint());
    }

    private Vector2 DirectionToWaypoint() {
        if(_closestWaypoint == null) {
            return default;
        }

        return (_closestWaypoint.transform.position - transform.position).normalized;
    }
}
