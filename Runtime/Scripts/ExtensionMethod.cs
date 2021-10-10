using System.Collections;
using ExpressoBits.Pools;
using UnityEngine;

public static class ExtensionMethods
{
    #region Instantiate Methods
    public static GameObject InstantiateFromPool(this MonoBehaviour value, GameObject original)
    {
        return PoolManager.Instantiate(original);
    }

    public static GameObject InstantiateFromPool(this MonoBehaviour value, Pool pool)
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
    public static void DestroyInPool(this MonoBehaviour value, GameObject gameObject, float t = 0f)
    {
        if (t <= 0f)
        {
            PoolManager.Destroy(gameObject);
            return;
        }
        value.StartCoroutine(DestroyInPool(gameObject, t));
    }

    private static IEnumerator DestroyInPool(GameObject gameObject, float t)
    {
        yield return new WaitForSeconds(t);
        PoolManager.Destroy(gameObject);
    }

    public static void DestroyInPool(this MonoBehaviour value, GameObject gameObject, Pool pool, float t = 0f)
    {
        if (t <= 0f)
        {
            pool.Destroy(gameObject);
            return;
        }
        value.StartCoroutine(DestroyInPool(gameObject, pool, t));
    }

    private static IEnumerator DestroyInPool(GameObject gameObject, Pool pool, float t)
    {
        yield return new WaitForSeconds(t);
        pool.Destroy(gameObject);
    }
    #endregion
}