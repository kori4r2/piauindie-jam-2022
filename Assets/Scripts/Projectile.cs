using UnityEngine;
using Toblerone.Toolbox;

public abstract class Projectile : MonoBehaviour, IPoolableObject {
    [SerializeField] protected Rigidbody2D rigidBody;
    [SerializeField] protected ProjectilePoolVariable pool;
    [SerializeField] protected bool destroyOnContact;
    protected Movable2D movable2D;
    protected Unit attacker;

    protected virtual void Awake() {
        movable2D = new Movable2D(rigidBody);
    }

    protected virtual void FixedUpdate() {
        movable2D.UpdateMovable();
    }

    public virtual void Launch(Unit attacker, Vector2 targetPosition) {
        this.attacker = attacker;
        movable2D.AllowDynamicMovement();
    }

    public virtual void InitObject() { }

    protected void OnTriggerEnter2D(Collider2D other) {
        if ((TryDealDamage(attacker, other.gameObject) && destroyOnContact) || other.CompareTag(CameraEdgeCollider.boundsTag)) {
            Destroy();
        }
    }

    public virtual void Destroy() {
        movable2D.BlockMovement();
        pool.Value.ReturnObjectToPool(this);
    }

    private static bool TryDealDamage(Unit attacker, GameObject collisionObj) {
        if (collisionObj.TryGetComponent(out IDamageable target)) {
            if (!CanDamage(attacker, target))
                return false;
            target.TakeDamage(attacker.Stats.AttackPower);
            return true;
        }
        return false;
    }

    private static bool CanDamage(Unit attacker, IDamageable target) {
        if (attacker is PlayerCharacter)
            return target is Enemy;
        if (attacker is Enemy)
            return !(target is Enemy);
        return false;
    }
}
