using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeProjectile : Projectile {
    [SerializeField] private Animator animator;
    [SerializeField] private string showAnimationBoolVariableName;

    public override void Launch(Unit attacker, Vector2 targetPosition) {
        base.Launch(attacker, targetPosition);
        movable2D.BlockMovement();
        animator.SetBool(showAnimationBoolVariableName, true);
    }

    public override void Destroy() {
        animator.SetBool(showAnimationBoolVariableName, false);
        base.Destroy();
    }
}
