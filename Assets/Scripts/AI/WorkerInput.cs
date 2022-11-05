using UnityEngine;

public class WorkerInput : AIInput {
    private Waypoint _nextWaypoint;

    private void Awake() {
        _nextWaypoint = FindClosestWaypoint();
    }

    private Waypoint FindClosestWaypoint() {
        var smallestDistance = float.MaxValue;
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
        if(_nextWaypoint == null) {
            return default;
        }

        return (_nextWaypoint.transform.position - transform.position).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Waypoint") {
            _nextWaypoint = collision.GetComponent<Waypoint>().NextWaypoint;
        }
    }
}
