using UnityEngine;
using Toblerone.Toolbox;

public abstract class Projectile : MonoBehaviour, IPoolableObject {
    [SerializeField] protected Rigidbody2D rigidbody;
    protected Movable2D movable2D;

    protected virtual void Awake() {
        movable2D = new Movable2D(rigidbody);
    }

    protected virtual void FixedUpdate() {
        movable2D.UpdateMovable();
    }

    public virtual void Configure(int attackPower, Vector2 targetPosition) {
    }

    public void InitObject() {
        throw new System.NotImplementedException();
    }
}
