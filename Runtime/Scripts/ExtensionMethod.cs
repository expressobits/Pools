using UnityEngine;
using ExpressoBits.Pools;

public static class ExtensionMethods
{
    #region Instantiate Methods
    public static GameObject InstantiateFromPool(this MonoBehaviour value, GameObject original)
    {
        return PoolManager.Instantiate(original);
    }

    public static GameObject InstantiateFromPool(this MonoBehaviour value,Pool pool)
    {
        return pool.Instantiate();
    }

    public static GameObject InstantiateFromPool(this MonoBehaviour value, GameObject original,
        Vector3 position, Quaternion rotation)
    {
        return PoolManager.Instantiate(original, position, rotation);
    }
    
    public static T InstantiateFromPool<T>(this MonoBehaviour value, T original,
        Vector3 position, Quaternion rotation) where T : Component
    {
        GameObject o = InstantiateFromPool(value, original.gameObject, position, rotation);
        // TODO Wrong perfomance
        return o.GetComponent<T>();
    }

    public static T InstantiateFromPool<T>(this MonoBehaviour value, Pool pool,
        Vector3 position, Quaternion rotation) where T : Component
    {
        GameObject o = InstantiateFromPool(value, pool, position, rotation);
        // TODO Wrong perfomance
        return o.GetComponent<T>();
    }
    
    public static GameObject InstantiateFromPool(this MonoBehaviour value, Pool pool,
        Vector3 position, Quaternion rotation)
    {
        return pool.Instantiate(position, rotation);
    }
    #endregion

    #region Destroy Methods
    public static void DestroyInPool(this MonoBehaviour value, GameObject gameObject)
    {
        PoolManager.Destroy(gameObject);
    }

    public static void DestroyInPool(this MonoBehaviour value, GameObject gameObject, Pool pool)
    {
        pool.Destroy(gameObject);
    }
    #endregion
}