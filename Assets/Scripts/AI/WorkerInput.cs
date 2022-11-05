using UnityEngine;

public class WorkerInput : AIInput {
    private Waypoint _nextWaypoint;
    private BuildingPlace _buildingPlace;

    private Unit _unit;

    private void Awake() {
        _nextWaypoint = FindClosestWaypoint();
    }

    private void OnEnable() {
        _unit = GetComponent<Unit>();
        _unit.OnDeath.AddListener(OnDeath);
    }

    private void OnDisable() {
        _unit.OnDeath.RemoveListener(OnDeath);
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
        // We don't move if we're helping constructing a build
        if(_buildingPlace != null) {
            return default;
        }

        if (_nextWaypoint == null) {
            return default;
        }

        return (_nextWaypoint.transform.position - transform.position).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        // If we hit a Waypoint, we want to know:
        // - If it points to a Building Place, then we are only interested in it if it is NOT complete yet
        // - If it is complete, then we skip it, getting it's next waypoint;
        // If the next waypoint is just another regular waypoint, then we just go towards it
        if (collision.tag == "Waypoint") {
            var waypoint = collision.GetComponent<Waypoint>();
            var buildingPlace = waypoint.BuildingPlace;
            if (buildingPlace != null && buildingPlace.IsComplete == true) {
                _nextWaypoint = waypoint.NextWaypoint?.NextWaypoint;
            } else {
                // If this waypoint is not a building place, then we just get the next position right away
                _nextWaypoint = waypoint.NextWaypoint;
            }
        }

        // If what we hit is a Building Place, then we want to stop there and give it XP if it's not yet complete
        // If it is complete, we continue to its next waypoint
        if (collision.tag == "BuildingPlace") {
            var buildingPlace = collision.GetComponent<BuildingPlace>();
            if (buildingPlace.IsComplete == false) {
                _buildingPlace = buildingPlace;
                _buildingPlace.OnWorkerArrived(GetComponent<Worker>());
            } else {
                // If this waypoint is not a building place, then we just get the next position right away
                _nextWaypoint = collision.GetComponent<Waypoint>().NextWaypoint;
            }
        }
    }

    private void OnDeath() {
        _buildingPlace.OnWorkerDied(GetComponent<Worker>());
    }
}
