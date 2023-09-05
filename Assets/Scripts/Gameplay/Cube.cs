using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Cube : MonoBehaviour, IProduct, IPoolable<Cube>
{
    private Coroutine rotationCoroutine;

    public ObjectPool<Cube> Pool => pool;
    private ObjectPool<Cube> pool;

    public void Initialize()
    {
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

    public void OnDespawned()
    {
        pool.Release(this);
        StopCoroutine(rotationCoroutine);
    }

    public void OnSpawned(ObjectPool<Cube> pool)
    {
        this.pool = pool;
    }
}
