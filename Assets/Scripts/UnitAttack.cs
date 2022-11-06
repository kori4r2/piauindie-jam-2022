using System;
using UnityEngine;

[Serializable]
public class UnitAttack {
    [SerializeField] private ProjectilePoolVariable pool;
    [SerializeField, Range(0, 10f)] private float attackCooldown;
    [SerializeField, Range(0, 10f)] private float attackSpawnDistance;
    [SerializeField] private AudioClip soundFX;
    public bool CanAttack => pool != null && timer <= 0;
    private Unit thisUnit;
    private float timer;

    public void Init(Unit unit) {
        thisUnit = unit;
        timer = 0;
    }

    public void Update(float deltaTime) {
        if (timer > 0)
            timer -= deltaTime;
    }

    public void Attack(Vector2 targetPosition) {
        if (!CanAttack)
            return;
        thisUnit.OnAttack.Invoke();
        timer = attackCooldown;
        SoundPlayer.Instance?.PlaySFX(soundFX);
        Projectile projectile = InstantiateProjectile(targetPosition);
        projectile.Launch(thisUnit, targetPosition);
    }

    private Projectile InstantiateProjectile(Vector2 targetPosition) {
        Vector2 thisUnitPosition = new Vector2(thisUnit.transform.position.x, thisUnit.transform.position.y);
        Vector2 attackDirection = (targetPosition - thisUnitPosition).normalized;
        Vector2 spawnLocation2D = thisUnitPosition + (attackSpawnDistance * attackDirection);
        Vector3 spawnLocation = new Vector3(spawnLocation2D.x, spawnLocation2D.y, thisUnit.transform.position.z);
        return pool.Value.InstantiateObject(spawnLocation, Quaternion.FromToRotation(Vector2.up, attackDirection));
    }
}
