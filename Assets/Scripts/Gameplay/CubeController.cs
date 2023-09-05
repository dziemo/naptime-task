using System;
using UnityEngine;
using UnityEngine.Pool;

public class CubeController : MonoBehaviour, IProduct, IPoolable<CubeController>
{
    public event Action<CubeController> OnHit;

    [SerializeField]
    private RandomRotator rotator;
    [SerializeField]
    private Shooting shooting;
    [SerializeField]
    private Health health;

    public int CurrentHealth => health.CurrentHealth;

    private ProjectilesManager projectilesManager;

    public ObjectPool<CubeController> Pool => pool;
    private ObjectPool<CubeController> pool;

    public void SetupDependencies(ProjectilesManager projectilesManager)
    {
        this.projectilesManager = projectilesManager;
    }

    public void Initialize()
    {
        shooting.Initialize(projectilesManager);
        health.Initialize();
        StartBehaviours();
    }

    private void Health_OnDamageTaken(int healthLeft)
    {
        if (healthLeft > 0)
        {
            BeingHit();
        }
        else
        {
            Die();
        }
    }

    private void BeingHit()
    {
        StopBehaviours();
        OnHit?.Invoke(this);
        gameObject.SetActive(false);
    }

    public void Reappear()
    {
        gameObject.SetActive(true);
        StartBehaviours();
    }

    private void Die()
    {
        StopBehaviours();
        OnDespawn();
        Debug.Log("DIED");
    }

    private void StartBehaviours()
    {
        rotator.StartRotation();
        shooting.StartShooting();
    }

    private void StopBehaviours()
    {
        rotator.StopRotation();
        shooting.StopShooting();
    }

    public void OnDespawn()
    {
        pool.Release(this);
        rotator.StopRotation();

        health.OnDamageTaken -= Health_OnDamageTaken;
    }

    public void OnSpawn(ObjectPool<CubeController> pool)
    {
        this.pool = pool;

        health.OnDamageTaken += Health_OnDamageTaken;
    }
}
