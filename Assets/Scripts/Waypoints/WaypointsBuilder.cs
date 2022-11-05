using UnityEngine;

public class WaypointsBuilder : MonoBehaviour {
    [SerializeField] private bool _drawGizmos;

    public void Awake() {
        // the last waypoint will never have a next one, of course
        for (int i = 0; i < transform.childCount - 1; i++) {
            var waypoint = transform.GetChild(i).GetComponent<Waypoint>();
            var nextWaypoint = transform.GetChild(i + 1).GetComponent<Waypoint>();
            waypoint.SetWaypoint(nextWaypoint);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos() {
        if(_drawGizmos == false) {
            return;
        }

        for (int i = 0; i < transform.childCount; i++) {
            var currentPos = transform.GetChild(i).position;
            Gizmos.DrawWireSphere(currentPos, .5f);

            if(i < transform.childCount - 1) {
                var nextPos = transform.GetChild(i + 1).position;
                Gizmos.DrawLine(currentPos, nextPos);
            }
        }
    }
#endif
}
