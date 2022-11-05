using UnityEngine;

public abstract class AIUnit : Unit {
    [SerializeField] protected AIInput _aiInput;

    protected override void Awake() {
        base.Awake();
        movable2D.AllowDynamicMovement();
    }

    private void OnEnable() {
        AddInputCallbacks();
    }

    private void AddInputCallbacks() {
        _aiInput.MovementPerformed += ReadMovementInput;
        _aiInput.AttackPerformed += OnAttackPerformed;
    }

    private void OnDisable() {
        _aiInput.MovementPerformed -= ReadMovementInput;
        _aiInput.AttackPerformed -= OnAttackPerformed;
    }

    private void ReadMovementInput(Vector2 direction) {
        movementDirection = direction;
    }

    protected virtual void OnAttackPerformed(Vector2 direction) {
    }
}
