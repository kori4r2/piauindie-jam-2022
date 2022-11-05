using UnityEngine;

public class Worker : Unit {
    [SerializeField]
    private int _grantedXP;
    public int GrantedEXP => _grantedXP;

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

    protected override void Update() {
        base.Update();
    }

    private void OnDisable() {
        _aiInput.MovementPerformed -= ReadMovementInput;
    }

    private void ReadMovementInput(Vector2 direction) {
        movementDirection = direction;
    }

    public override void TakeDamage(int damage) {
    }
}
