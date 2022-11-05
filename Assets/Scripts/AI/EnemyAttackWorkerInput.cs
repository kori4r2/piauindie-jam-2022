using UnityEngine;

public class EnemyAttackWorkerInput : AIInput {
    [SerializeField] private AIUnitRuntimeSet workers;
    [SerializeField, Range(0f, 10f)] private float _stopDistance = 5f;
    [SerializeField] private float _attackInterval;
    private float _attackTimer;
    private Worker currentTarget = null;

    private void Awake() {
        _attackTimer = _attackInterval;
    }

    public void Update() {
        if (currentTarget == null)
            SelectNewTarget();
        bool reachedTarget = MoveTowardsTarget();
        if (reachedTarget == true) {
            _attackTimer -= Time.deltaTime;
            HandleAttacking();
        }
    }

    private void SelectNewTarget() {
        AIUnit[] workerArray = workers.ToArray();
        int selectedIndex = UnityEngine.Random.Range(0, workerArray.Length);
        currentTarget = workerArray[selectedIndex] as Worker;
        currentTarget.OnDeath.AddListener(LoseTarget);
        currentTarget.OnVanish.AddListener(LoseTarget);
    }

    private void LoseTarget() {
        currentTarget = null;
    }

    private bool MoveTowardsTarget() {
        if (currentTarget == null) {
            MovementPerformed.Invoke(Vector2.zero);
            return false;
        }

        bool reachedTarget = false;

        Vector3 targetPosition = currentTarget.transform.position;
        Vector3 offset = targetPosition - transform.position;
        Vector2 direction = Vector2.zero;

        if (offset.magnitude > _stopDistance) {
            direction = offset.normalized;
        } else {
            reachedTarget = true;
        }

        MovementPerformed.Invoke(direction);
        return reachedTarget;
    }

    private void HandleAttacking() {
        if (_attackTimer > 0)
            return;

        _attackTimer = _attackInterval;
        Vector3 targetPosition = currentTarget.transform.position;
        AttackPerformed.Invoke(targetPosition);
    }
}
