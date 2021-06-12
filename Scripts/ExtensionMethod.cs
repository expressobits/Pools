using UnityEngine;
using ExpressoBits.Pools;

public static class ExtensionMethods
{
    public static GameObject InstantiateInPool(this MonoBehaviour value, GameObject original)
    {
        return PoolManager.Instantiate(original);
    }

    public static GameObject InstantiateInPool(this MonoBehaviour value, GameObject original, Pool pool)
    {
        return pool.Dequeue(original);
    }

    public static GameObject InstantiateInPool(this MonoBehaviour value, GameObject original,
        Vector3 position, Quaternion rotation)
    {
        return PoolManager.Instantiate(original, position, rotation);
    }
    
    public static T InstantiateInPool<T>(this MonoBehaviour value, T original,
        Vector3 position, Quaternion rotation) where T : Component
    {
        GameObject o = PoolManager.Instantiate(original.gameObject, position, rotation);
        // TODO Wrong perfomance
        return o.GetComponent<T>();
    }
    
    public static GameObject InstantiateInPool(this MonoBehaviour value, GameObject original, Pool pool,
        Vector3 position, Quaternion rotation)
    {
        return pool.Dequeue(original, position, rotation);
    }

    public static void DestroyInPool(this MonoBehaviour value, GameObject gameObject)
    {
        PoolManager.Destroy(gameObject);
    }

    public static void DestroyInPool(this MonoBehaviour value, GameObject gameObject, Pool pool)
    {
        pool.Enqueue(gameObject);
    }
}