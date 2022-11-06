using UnityEngine;
using UnityEngine.Events;
using Toblerone.Toolbox;

public abstract class Unit : MonoBehaviour, IDamageable {
    [SerializeField] UnitAnimation unitAnimation;
    [SerializeField] protected Rigidbody2D rigidBody;
    [SerializeField] protected UnitStats unitStats;
    public UnitStats Stats => unitStats;
    [SerializeField] protected UnitAttack unitAttack;
    public readonly UnityEvent OnDeath = new UnityEvent();
    public readonly UnityEvent<int> OnTakeDamage = new UnityEvent<int>();
    public readonly UnityEvent OnAttack = new UnityEvent();
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
        else
            OnTakeDamage.Invoke(damage);
    }

    protected virtual void Die() {
        OnDeath.Invoke();
        Destroy(this.gameObject); // maybe do object pooling logic instead - might be overengineering for a game jam though
    }

    protected virtual void ProcessMovementInput() {
        Vector2 newVelocity = Stats.MoveSpeed * movementDirection;
        movable2D.SetVelocity(newVelocity);
        unitAnimation.Update(newVelocity);
    }

    protected virtual void Awake() {
        currentHP = Stats.MaxHP;
        movable2D = new Movable2D(rigidBody);
        movable2D.BlockMovement();
        unitAttack.Init(this);
        unitAnimation.Setup(this);
    }

    protected virtual void FixedUpdate() {
        movable2D.UpdateMovable();
    }
}
