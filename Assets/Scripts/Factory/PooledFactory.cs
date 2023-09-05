using UnityEngine;
using UnityEngine.Pool;

public abstract class PooledFactory<T> : MonoBehaviour where T : MonoBehaviour, IProduct, IPoolable<T>
{
    [SerializeField]
    private T productPrefab;

    private ObjectPool<T> productPool;

    private void Awake()
    {
        productPool = new ObjectPool<T>(
            () => OnCreate(),
            product => OnGetProduct(product),
            product => OnReleaseProduct(product),
            product => OnDestroyProduct(product));
    }

    protected virtual void OnGetProduct(T product)
    {
        product.gameObject.SetActive(true);
    }

    protected virtual void OnReleaseProduct(T product)
    {
        product.gameObject.SetActive(false);
    }

    protected virtual void OnDestroyProduct(T product) { }

    protected virtual T OnCreate()
    {
        T newProduct = Instantiate(productPrefab);
        newProduct.OnSpawned(productPool);
        return newProduct;
    }

    public virtual T GetProduct()
    {
        return productPool.Get();
    }
}
