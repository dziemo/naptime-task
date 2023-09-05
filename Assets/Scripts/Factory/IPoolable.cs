using UnityEngine;
using UnityEngine.Pool;

public interface IPoolable<T> where T : MonoBehaviour
{
    public ObjectPool<T> Pool { get; }
    public void OnDespawn();
    public void OnSpawn(ObjectPool<T> pool);
}
