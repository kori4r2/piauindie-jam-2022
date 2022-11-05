using UnityEngine;

public class EnemyInput : AIInput {
    [SerializeField, Range(0f, 10f)] private float _stopDistance = 5f;

    public void Update() {
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer() {
        var playerPosition = _playerRef.Value.transform.position;
        var offset = playerPosition - transform.position;
        var direction = default(Vector2);

        // We stop moving if we're close enough to the player
        if (offset.magnitude > _stopDistance) {
            direction = offset.normalized;
        }

        MovementPerformed.Invoke(direction);
    }
}
