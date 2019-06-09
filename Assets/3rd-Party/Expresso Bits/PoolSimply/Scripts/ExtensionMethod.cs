using UnityEngine;
using System;
using ExpressoBits.PoolSimply;

public static class ExtensionMethods
{
    public static GameObject InstantiateInPool(this MonoBehaviour value, GameObject gameObject, Pool pool)
    {
        if(pool.enabled){
            return pool.Dequeue(gameObject);
        }else{
            return GameObject.Instantiate(gameObject);
        }
        
    }

    public static GameObject InstantiateInPool(this MonoBehaviour value, GameObject gameObject, 
    Vector3 position, Quaternion rotation, Pool pool)
    {
        if(pool.enabled){
            return pool.Dequeue(gameObject,position,rotation);
        }else{
            return GameObject.Instantiate(gameObject,position,rotation);
        }
    }

    public static void DestroyInPool(this MonoBehaviour value, GameObject gameObject, Pool pool)
    {
        if(pool.enabled){
            pool.Enqueue(gameObject);
        }else{
            GameObject.Destroy(gameObject);
        }
    }

}

