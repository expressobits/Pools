using UnityEngine;
using ExpressoBits.Pools;

public static class ExtensionMethods
{
    #region Instantiate Methods
    public static GameObject InstantiateInPool(this MonoBehaviour value, GameObject original)
    {
        return PoolManager.Instantiate(original);
    }

    public static GameObject InstantiateInPool(this MonoBehaviour value,Pool poolData)
    {
        return poolData.Dequeue();
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
    
    public static GameObject InstantiateInPool(this MonoBehaviour value, Pool poolData,
        Vector3 position, Quaternion rotation)
    {
        return poolData.Dequeue(position, rotation);
    }
    #endregion

    #region Destroy Methods
    public static void DestroyInPool(this MonoBehaviour value, GameObject gameObject)
    {
        PoolManager.Destroy(gameObject);
    }

    public static void DestroyInPool(this MonoBehaviour value, GameObject gameObject, Pool pool)
    {
        pool.Enqueue(gameObject);
    }
    #endregion
}