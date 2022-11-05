using UnityEngine;

public class BasicRangedProjectile : Projectile {
    [SerializeField] private float moveSpeed;

    public override void Launch(Unit attacker, Vector2 targetPosition) {
        base.Launch(attacker, targetPosition);
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        movable2D.SetVelocity(moveSpeed * (targetPosition - currentPosition).normalized);
    }
}
