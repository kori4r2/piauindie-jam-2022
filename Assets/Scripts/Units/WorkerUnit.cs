using UnityEngine;

public class WorkerUnit : Unit {
    [SerializeField]
    private int _grantedXP;

    public int GrantedXP => _grantedXP;

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
    }

    private void ReadMovementInput(Vector2 direction) {
        movementDirection = direction;
    }
}
