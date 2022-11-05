using UnityEngine;
using UnityEngine.Events;
using Toblerone.Toolbox;

public abstract class Unit : MonoBehaviour, IDamageable {
    [SerializeField] protected Rigidbody2D rigidBody;
    [SerializeField] protected UnitStats unitStats;
    public UnitStats Stats => unitStats;
    [SerializeField] protected UnitAttack unitAttack;
    public readonly UnityEvent OnDeath = new UnityEvent();
    public readonly UnityEvent<int> OnTakeDamage = new UnityEvent<int>();
    protected Movable2D movable2D;
    protected Vector2 movementDirection;
    protected int currentHP;

    protected virtual void Update() {
        ProcessMovementInput();
    }

    public virtual void TakeDamage(int damage) {
        currentHP -= damage;
        if (currentHP <= 0)
            Die();
    }

    protected virtual void Die() {
        OnDeath.Invoke();
        Destroy(this.gameObject); // maybe do object pooling logic instead - might be overengineering for a game jam though
    }

    protected virtual void ProcessMovementInput() {
        movable2D.SetVelocity(Stats.MoveSpeed * movementDirection);
    }

    protected virtual void Awake() {
        movable2D = new Movable2D(rigidBody);
        movable2D.BlockMovement();
        unitAttack.Init(this);
        currentHP = Stats.MaxHP;
    }

    protected virtual void FixedUpdate() {
        movable2D.UpdateMovable();
    }
}
