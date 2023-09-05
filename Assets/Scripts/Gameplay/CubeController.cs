using System;
using UnityEngine;
using UnityEngine.Pool;

public class CubeController : MonoBehaviour, IProduct, IPoolable<CubeController>
{
    public event Action<CubeController> OnDamageTaken;

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

    private void OnEnable()
    {
        health.OnDamageTaken += Health_OnDamageTaken;
    }

    private void OnDisable()
    {
        health.OnDamageTaken -= Health_OnDamageTaken;
    }

    private void Health_OnDamageTaken(int healthLeft)
    {
        OnDamageTaken?.Invoke(this);

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
    }

    private void StartBehaviours()
    {
        rotator.StartRotation();
        shooting.StartShooting();
    }

    public void StopBehaviours()
    {
        rotator.StopRotation();
        shooting.StopShooting();
    }

    public void OnDespawn()
    {
        pool.Release(this);
        rotator.StopRotation();
    }

    public void OnSpawn(ObjectPool<CubeController> pool)
    {
        this.pool = pool;
    }
}
