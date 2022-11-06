using System;
using UnityEngine;

[Serializable]
public class UnitAttack {
    [SerializeField] private ProjectilePoolVariable pool;
    [SerializeField, Range(0, 10f)] private float attackSpawnDistance;
    private Unit thisUnit;

    public void Init(Unit unit) {
        thisUnit = unit;
    }

    public void Attack(Vector2 targetPosition) {
        if (pool == null)
            return;
        thisUnit.OnAttack.Invoke();
        Vector2 thisUnitPosition = new Vector2(thisUnit.transform.position.x, thisUnit.transform.position.y);
        Vector2 attackDirection = (targetPosition - thisUnitPosition).normalized;
        Vector2 spawnLocation2D = thisUnitPosition + (attackSpawnDistance * attackDirection);
        Vector3 spawnLocation = new Vector3(spawnLocation2D.x, spawnLocation2D.y, thisUnit.transform.position.z);
        Projectile projectile = pool.Value.InstantiateObject(spawnLocation, Quaternion.FromToRotation(Vector2.up, attackDirection));
        projectile.Launch(thisUnit, targetPosition);
    }
}
