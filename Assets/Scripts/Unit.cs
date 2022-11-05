using UnityEngine;
using Toblerone.Toolbox;

public abstract class Unit : MonoBehaviour {
    [SerializeField] protected Rigidbody2D rigidBody;
    [SerializeField] protected UnitStats unitStats;
    protected Movable2D movable2D;

    protected virtual void Update() {
        ProcessMovementInput();
    }

    protected abstract void ProcessMovementInput();

    protected virtual void Awake() {
        movable2D = new Movable2D(rigidBody);
        movable2D.BlockMovement();
    }

    protected virtual void FixedUpdate() {
        movable2D.UpdateMovable();
    }
}
