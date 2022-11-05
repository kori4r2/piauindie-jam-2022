using UnityEngine;
using Toblerone.Toolbox;

public abstract class Unit : MonoBehaviour {
    [SerializeField] protected Rigidbody2D rigidBody;
    [SerializeField] protected UnitStats unitStats;
    public UnitStats Stats => unitStats;
    [SerializeField] protected UnitAttack unitAttack;
    protected Movable2D movable2D;

    protected Vector2 movementDirection;
    [SerializeField, Range(0f, 20f)] protected float moveSpeed = 5f;

    protected virtual void Update() {
        ProcessMovementInput();
    }

    public abstract void TakeDamage(int damage);

    protected virtual void ProcessMovementInput() {
        movable2D.SetVelocity(moveSpeed * movementDirection);
    }

    protected virtual void Awake() {
        movable2D = new Movable2D(rigidBody);
        movable2D.BlockMovement();
        unitAttack.Init(this);
    }

    protected virtual void FixedUpdate() {
        movable2D.UpdateMovable();
    }
}
