using System;
using UnityEngine;

[Serializable]
public class UnitAnimation {
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField, Range(1f, 20f)] private float expectedSpeed = 5f;
    private const string takeDamageAnimString = "TakeDamage";
    private const string speedAnimString = "speed";
    private const string attackAnimString = "Attack";
    private const string isMovingAnimString = "isMoving";
    private bool lookingToTheRight;
    private Unit thisUnit;

    public void Setup(Unit unit) {
        unit.OnTakeDamage.AddListener(_ => animator.SetTrigger(takeDamageAnimString));
        unit.OnAttack.AddListener(() => animator.SetTrigger(attackAnimString));
        lookingToTheRight = true;
        thisUnit = unit;
    }

    public void Update(Vector2 currentSpeed) {
        if (ShouldTurnAround(currentSpeed.x)) {
            lookingToTheRight = !lookingToTheRight;
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
        animator.SetBool(isMovingAnimString, currentSpeed.sqrMagnitude > float.Epsilon);
        float speed = Mathf.Clamp(currentSpeed.magnitude, expectedSpeed / 2f, expectedSpeed * 2f);
        animator.SetFloat(speedAnimString, speed / expectedSpeed);
    }

    private bool ShouldTurnAround(float speedX) {
        if (Mathf.Abs(speedX) < thisUnit.Stats.MoveSpeed * Mathf.Cos(Mathf.Deg2Rad * 89))
            return false;
        return (lookingToTheRight && speedX < 0) || (!lookingToTheRight && speedX > 0);
    }
}
