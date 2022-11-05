using UnityEngine;

public class WorkerInput : AIInput {
    private Waypoint _nextWaypoint;
    private BuildingPlace _buildingPlace;

    [SerializeField] private Unit _unit;
    private Worker _worker;
    [SerializeField] private WaypointRuntimeSet waypoints;

    private void Awake() {
        _worker = GetComponent<Worker>();
    }

    private void Start() {
        _nextWaypoint = FindClosestWaypoint();
    }

    private void OnEnable() {
        _unit.OnDeath.AddListener(OnDeath);
    }

    private void OnDisable() {
        _unit.OnDeath.RemoveListener(OnDeath);
    }

    private Waypoint FindClosestWaypoint() {
        float smallestDistance = float.MaxValue;
        Waypoint chosenWaypoint = null;

        Waypoint[] allWaypoints = waypoints.ToArray();
        for (int i = 0; i < allWaypoints.Length; i++) {
            float distanceToWaypoint = Vector3.Distance(transform.position, allWaypoints[i].transform.position);
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
        if (_buildingPlace != null) {
            return Vector2.zero;
        }

        if (_nextWaypoint == null) {
            return Vector2.zero;
        }

        return (_nextWaypoint.transform.position - transform.position).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        // If we hit a Waypoint, we want to know:
        // - If it points to a Building Place, then we are only interested in it if it is NOT complete yet
        // - If it is complete, then we skip it, getting it's next waypoint;
        // If the next waypoint is just another regular waypoint, then we just go towards it
        if (collision.CompareTag("Waypoint")) {
            Waypoint waypoint = collision.GetComponent<Waypoint>();
            BuildingPlace buildingPlace = waypoint.BuildingPlace;
            if (buildingPlace != null && buildingPlace.IsComplete == true) {
                _nextWaypoint = waypoint.NextWaypoint != null ? waypoint.NextWaypoint.NextWaypoint : null;
            } else {
                // If this waypoint is not a building place, then we just get the next position right away
                _nextWaypoint = waypoint.NextWaypoint;
            }
        }

        // If what we hit is a Building Place, then we want to stop there and give it XP if it's not yet complete
        // If it is complete, we continue to its next waypoint
        if (collision.CompareTag("BuildingPlace")) {
            BuildingPlace buildingPlace = collision.GetComponent<BuildingPlace>();
            if (buildingPlace.IsComplete == false) {
                _buildingPlace = buildingPlace;
                _buildingPlace.OnWorkerArrived(_worker);
            } else {
                // If this waypoint is not a building place, then we just get the next position right away
                _nextWaypoint = collision.GetComponent<Waypoint>().NextWaypoint;
            }
        }
    }

    private void OnDeath() {
        if (_buildingPlace == null)
            return;
        _buildingPlace.OnWorkerDied(_worker);
    }
}
