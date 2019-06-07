using UnityEngine;
using System;
using ExpressoBits.PoolSimply;

public static class ExtensionMethods
{
    public static GameObject InstantiateInPool(this MonoBehaviour value, GameObject gameObject)
    {
        return Pools.Instance.Dequeue(gameObject);
    }

    public static void DestroyInPool(this MonoBehaviour value, GameObject gameObject)
    {
        Pools.Instance.Enqueue(gameObject);
    }

}

