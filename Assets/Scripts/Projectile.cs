using System.Collections.Generic;
using UnityEngine;
using Toblerone.Toolbox;

public abstract class Projectile : MonoBehaviour, IPoolableObject {
    [SerializeField] protected Rigidbody2D rigidBody;
    [SerializeField] protected ProjectilePoolVariable pool;
    [SerializeField] protected bool destroyOnContact;
    [SerializeField] protected AudioClip impactSFX;
    protected Movable2D movable2D;
    protected Unit attacker;
    protected HashSet<Collider2D> collidersChecked;

    protected virtual void Awake() {
        movable2D = new Movable2D(rigidBody);
        collidersChecked = new HashSet<Collider2D>();
    }

    protected virtual void FixedUpdate() {
        movable2D.UpdateMovable();
    }

    public virtual void Launch(Unit attacker, Vector2 targetPosition) {
        this.attacker = attacker;
        movable2D.AllowDynamicMovement();
    }

    public virtual void InitObject() {
        collidersChecked.Clear();
    }

    protected void OnTriggerEnter2D(Collider2D other) {
        if (collidersChecked.Contains(other))
            return;
        collidersChecked.Add(other);
        if ((TryDealDamage(attacker, other.gameObject) && destroyOnContact) || other.CompareTag(CameraEdgeCollider.boundsTag)) {
            Destroy();
        }
    }

    public virtual void Destroy() {
        movable2D.BlockMovement();
        pool.Value.ReturnObjectToPool(this);
    }

    private bool TryDealDamage(Unit attacker, GameObject collisionObj) {
        if (collisionObj.TryGetComponent(out IDamageable target)) {
            if (!CanDamage(attacker, target))
                return false;
            SoundPlayer.Instance?.PlaySFX(impactSFX);
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
