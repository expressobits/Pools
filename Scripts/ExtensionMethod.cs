using UnityEngine;
using ExpressoBits.Pools;

public static class ExtensionMethods
{
    public static GameObject InstantiateInPool(this MonoBehaviour value, GameObject gameObject)
    {
        return PoolManager.Instance().Instantiate(gameObject);
    }

    public static GameObject InstantiateInPool(this MonoBehaviour value, GameObject gameObject, Pool pool)
    {
        return pool.Dequeue(gameObject);
    }

    public static GameObject InstantiateInPool(this MonoBehaviour value, GameObject gameObject,
        Vector3 position, Quaternion rotation)
    {
        return PoolManager.Instance().Instantiate(gameObject, position, rotation);
    }

    public static GameObject InstantiateInPool(this MonoBehaviour value, GameObject gameObject, Pool pool,
        Vector3 position, Quaternion rotation)
    {
        return pool.Dequeue(gameObject, position, rotation);
    }

    public static void DestroyInPool(this MonoBehaviour value, GameObject gameObject)
    {
        PoolManager.Instance().Destroy(gameObject);
    }

    public static void DestroyInPool(this MonoBehaviour value, GameObject gameObject, Pool pool)
    {
        pool.Enqueue(gameObject);
    }
}