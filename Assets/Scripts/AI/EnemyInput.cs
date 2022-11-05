using UnityEngine;

public class EnemyInput : AIInput {
    [SerializeField, Range(0f, 10f)] private float _stopDistance = 5f;

    [SerializeField] private float _attackInterval;
    private float _attackTimer;

    public void Update() {
        MoveTowardsPlayer(out var reachedPlayer);

        _attackTimer -= Time.deltaTime;
        if (reachedPlayer == true) {
            HandleAttacking();
        }
    }

    private void MoveTowardsPlayer(out bool reachedPlayer) {
        reachedPlayer = false;

        var playerPosition = _playerRef.Value.transform.position;
        var offset = playerPosition - transform.position;
        var direction = default(Vector2);

        // We keep moving while we're far away from the player
        if (offset.magnitude > _stopDistance) {
            direction = offset.normalized;
        } else {
            reachedPlayer = true;
        }

        MovementPerformed.Invoke(direction);
    }

    private void HandleAttacking() {
        if(_attackTimer <= 0) {
            _attackTimer = _attackInterval;

            var playerPosition = _playerRef.Value.transform.position;
            var direction = (playerPosition - transform.position).normalized;
            AttackPerformed.Invoke(direction);
        }
    }
}
