using System;
using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour, IProduct, IPoolable<Projectile>
{
    public event Action<Projectile> OnDespawned;

    [SerializeField]
    private LayerMask targetMask;
    [SerializeField]
    private float checkRadius;
    [SerializeField]
    private float speed;

    private ObjectPool<Projectile> pool;
    public ObjectPool<Projectile> Pool => pool;

    private Vector3 direction;
    private Collider2D ownerCollider;

    public void Initialize()
    {
    }

    public void Launch(Vector3 position, Vector2 direction, Collider2D ownerCollider)
    {
        transform.position = position;
        this.direction = direction;
        this.ownerCollider = ownerCollider;
    }

    public void MoveProjectile()
    {
        transform.position += speed * Time.deltaTime * direction;
    }

    public void CheckForCollision()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, checkRadius, targetMask);
        if (collider != null && ownerCollider != collider && collider.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage();
            OnDespawn();
        }
    }

    public void OnDespawn()
    {
        pool.Release(this);
        OnDespawned?.Invoke(this);
    }

    public void OnSpawn(ObjectPool<Projectile> pool)
    {
        this.pool = pool;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
