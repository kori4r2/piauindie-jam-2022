using UnityEngine;
using Toblerone.Toolbox;

public class ProjectilePool : ObjectPool<Projectile> {
    [SerializeField] private Projectile projectilePrefab;
    protected override Projectile ObjectPrefab => projectilePrefab;
    [SerializeField] private ProjectileEvent despawnedObjectEvent;
    protected override GenericEvent<Projectile> DespawnedObjectEvent => despawnedObjectEvent;
}
