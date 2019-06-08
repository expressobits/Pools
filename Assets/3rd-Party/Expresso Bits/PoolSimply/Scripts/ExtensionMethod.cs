using UnityEngine;
using System;
using ExpressoBits.PoolSimply;

public static class ExtensionMethods
{
    public static GameObject InstantiateInPool(this MonoBehaviour value, GameObject gameObject)
    {
        Pools pools = gameObject.GetComponent<Pooler>().pools;
        return InstantiateInPool(value,gameObject,pools);
    }

    public static void DestroyInPool(this MonoBehaviour value, GameObject gameObject)
    {
        Pools pools = gameObject.GetComponent<Pooler>().pools;
        DestroyInPool(value,gameObject,pools);
    }

    public static GameObject InstantiateInPool(this MonoBehaviour value, GameObject gameObject, Pools pools)
    {
        return pools.Dequeue(gameObject);
    }

    public static void DestroyInPool(this MonoBehaviour value, GameObject gameObject, Pools pools)
    {
        pools.Enqueue(gameObject);
    }

}

