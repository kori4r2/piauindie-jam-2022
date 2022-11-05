using UnityEngine;

public abstract class AIUnit : Unit {
    [SerializeField] protected AIInput _aiInput;
    [SerializeField] private AIUnitRuntimeSet _runtimeSet;

    protected override void Awake() {
        base.Awake();
        movable2D.AllowDynamicMovement();
    }

    private void OnEnable() {
        AddInputCallbacks();
        _runtimeSet.AddElement(this);
    }

    private void AddInputCallbacks() {
        _aiInput.MovementPerformed.AddListener(ReadMovementInput);
        _aiInput.AttackPerformed.AddListener(OnAttackPerformed);
    }

    private void OnDisable() {
        RemoveInputCallbacks();
        _runtimeSet.RemoveElement(this);
    }

    private void RemoveInputCallbacks() {
        _aiInput.MovementPerformed.RemoveListener(ReadMovementInput);
        _aiInput.AttackPerformed.RemoveListener(OnAttackPerformed);
    }

    private void ReadMovementInput(Vector2 direction) {
        movementDirection = direction;
    }

    protected virtual void OnAttackPerformed(Vector2 targetPosition) {
        unitAttack.Attack(targetPosition);
    }
}
