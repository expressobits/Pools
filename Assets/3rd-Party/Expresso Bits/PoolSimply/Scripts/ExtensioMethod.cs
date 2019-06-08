using UnityEngine;
using System;
using ExpressoBits.PoolSimply;

public static class ExtensionMethods
{
    public static GameObject InstantiateInPool(this MonoBehaviour value, GameObject gameObject)
    {
        Pools pools = value.GetComponent<Pooler>().pools;
        return pools.Dequeue(gameObject);
    }

    public static void DestroyInPool(this MonoBehaviour value, GameObject gameObject)
    {
        Pools pools = value.GetComponent<Pooler>().pools;
        pools.Enqueue(gameObject);
    }

}

