using UnityEngine;
using System;
using ExpressoBits.PoolSimply;

public static class ExtensionMethods
{
    public static GameObject InstantiateInPool(this MonoBehaviour value, GameObject gameObject, Pool pool)
    {
        return pool.Dequeue(gameObject);
    }

    public static GameObject InstantiateInPool(this MonoBehaviour value, GameObject gameObject, 
    Vector3 position, Quaternion rotation, Pool pool)
    {
        return pool.Dequeue(gameObject,position,rotation);
    }

    public static void DestroyInPool(this MonoBehaviour value, GameObject gameObject, Pool pool)
    {
        pool.Enqueue(gameObject);
    }

}

