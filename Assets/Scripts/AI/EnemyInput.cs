using UnityEngine;

public class EnemyInput : AIInput {
    [SerializeField, Range(0f, 10f)] private float _stopDistance = 5f;

    [SerializeField] private float _attackInterval;
    private float _attackTimer;

    private void Awake() {
        _attackTimer = _attackInterval;
    }

    public void Update() {
        bool reachedPlayer = MoveTowardsPlayer();
        if (reachedPlayer == true) {
            _attackTimer -= Time.deltaTime;
            HandleAttacking();
        }
    }

    private bool MoveTowardsPlayer() {
        if (_playerRef.Value == null) {
            MovementPerformed.Invoke(Vector2.zero);
            return false;
        }

        bool reachedPlayer = false;

        Vector3 playerPosition = _playerRef.Value.transform.position;
        Vector3 offset = playerPosition - transform.position;
        Vector2 direction = Vector2.zero;

        if (offset.magnitude > _stopDistance) {
            direction = offset.normalized;
        } else {
            reachedPlayer = true;
        }

        MovementPerformed.Invoke(direction);
        return reachedPlayer;
    }

    private void HandleAttacking() {
        if (_attackTimer > 0)
            return;

        _attackTimer = _attackInterval;
        Vector3 playerPosition = _playerRef.Value.transform.position;
        AttackPerformed.Invoke(playerPosition);
    }
}
