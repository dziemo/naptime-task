using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Cube : MonoBehaviour, IProduct, IPoolable<Cube>
{
    [SerializeField]
    private Health health;
    [SerializeField]
    private float hitHideTime;

    private Coroutine rotationCoroutine;

    public ObjectPool<Cube> Pool => pool;
    private ObjectPool<Cube> pool;

    public void Initialize()
    {
        health.Initialize();
        rotationCoroutine = StartCoroutine(RotationSequence());
    }

    private IEnumerator RotationSequence()
    {
        yield return new WaitForSeconds(Random.Range(0f, 1f));
        Rotate();
        rotationCoroutine = StartCoroutine(RotationSequence());
    }

    private void Rotate()
    {
        transform.Rotate(0, 0, Random.Range(0f, 360f));
    }

    public void OnDespawn()
    {
        pool.Release(this);
        StopCoroutine(rotationCoroutine);

        health.OnDamageTaken -= Health_OnDamageTaken;
    }

    public void OnSpawn(ObjectPool<Cube> pool)
    {
        this.pool = pool;

        health.OnDamageTaken += Health_OnDamageTaken;
    }

    private void Health_OnDamageTaken(int healthLeft)
    {
        if (healthLeft > 0)
        {
            StartCoroutine(OnHit());
        }
        else
        {
            Die();
        }
    }

    private IEnumerator OnHit()
    {
        gameObject.SetActive(false);
        StopCoroutine(rotationCoroutine);
        yield return new WaitForSeconds(hitHideTime);
        gameObject.SetActive(true);
        rotationCoroutine = StartCoroutine(RotationSequence());
    }

    private void Die()
    {
        //Dead
    }
}
