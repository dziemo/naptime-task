using UnityEngine;
using UnityEngine.Pool;

public interface IPoolable<T> where T : MonoBehaviour
{
    public ObjectPool<T> Pool { get; }
    public void OnDespawned();
    public void OnSpawned(ObjectPool<T> pool);
}
