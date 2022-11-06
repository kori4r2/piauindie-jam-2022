using UnityEngine;
using UnityEngine.Events;
using Toblerone.Toolbox;

public abstract class Unit : MonoBehaviour, IDamageable {
    [SerializeField] protected Rigidbody2D rigidBody;
    [SerializeField] protected UnitStats unitStats;
    public UnitStats Stats => unitStats;
    [SerializeField] protected UnitAttack unitAttack;
    [SerializeField] UnitAnimation unitAnimation;
    [SerializeField] AudioClip takeDamageSFX;
    [SerializeField] protected BoolEvent gameOverWithVictory;
    protected GenericEventListener<bool> gameOverListener;
    public readonly UnityEvent OnDeath = new UnityEvent();
    public readonly UnityEvent<int> OnTakeDamage = new UnityEvent<int>();
    public readonly UnityEvent OnAttack = new UnityEvent();
    public bool CanAttack => unitAttack.CanAttack;
    protected Movable2D movable2D;
    protected Vector2 movementDirection;
    protected int currentHP;
    protected bool gameIsOver;

    protected virtual void Awake() {
        currentHP = Stats.MaxHP;
        movable2D = new Movable2D(rigidBody);
        movable2D.BlockMovement();
        unitAttack.Init(this);
        unitAnimation.Setup(this);
        OnTakeDamage.AddListener(_ => SoundPlayer.Instance?.PlaySFX(takeDamageSFX));
        PrepareForGameOver();
    }

    private void PrepareForGameOver() {
        gameIsOver = false;
        gameOverListener = new GenericEventListener<bool>(gameOverWithVictory, OnGameOver);
        gameOverListener.StartListeningEvent();
    }

    protected virtual void Update() {
        if (gameIsOver)
            return;
        ProcessMovementInput();
        unitAttack.Update(Time.deltaTime);
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
        if (gameIsOver)
            return;
        Vector2 newVelocity = Stats.MoveSpeed * movementDirection;
        movable2D.SetVelocity(newVelocity);
        unitAnimation.Update(newVelocity);
    }

    protected virtual void OnGameOver(bool isVictory) {
        gameIsOver = true;
    }

    protected virtual void FixedUpdate() {
        if (gameIsOver)
            return;
        movable2D.UpdateMovable();
    }
}
