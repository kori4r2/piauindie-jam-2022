using UnityEngine;
using Toblerone.Toolbox;

public class ProjectilePool : ObjectPool<Projectile> {
    [SerializeField] private Projectile projectilePrefab;
    protected override Projectile ObjectPrefab => projectilePrefab;
    [SerializeField] private ProjectileEvent despawnedObjectEvent;
    protected override GenericEvent<Projectile> DespawnedObjectEvent => despawnedObjectEvent;
    [SerializeField] private ProjectilePoolVariable reference;

    protected override void OnEnable() {
        base.OnEnable();
        reference.Value = this;
    }

    protected override void OnDisable() {
        base.OnDisable();
        if (reference.Value == this)
            reference.Value = null;
    }
}
