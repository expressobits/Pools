using UnityEngine;
using System.Collections.Generic;

namespace ExpressoBits.Pools
{
    public class PoolManager
    {
        private static readonly Dictionary<GameObject, Pool> PoolsFromPrefab = new Dictionary<GameObject, Pool>();
        private static readonly Dictionary<GameObject, Pool> PoolsFromObjects = new Dictionary<GameObject, Pool>();
        private const uint defaultIncreaseSize = 5;

        #region Basic Functions
        public static GameObject Instantiate(GameObject prefab, Pool pool, Vector3 position, Quaternion rotation)
        {
            return pool.Dequeue(prefab, position, rotation);
        }
        public static void Destroy(GameObject obj, Pool pool)
        {
            pool.Enqueue(obj);
        }
        #endregion

        #region Variants
        public static GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            if (!PoolsFromPrefab.TryGetValue(prefab, out var pool))
            {
                PoolSettings settings;
                if (prefab.TryGetComponent(out Pooler pooler))
                {
                    settings = pooler.poolSettings;
                }
                else
                {
                    settings = new PoolSettings{ increaseSize = defaultIncreaseSize };
                }
                pool = new Pool(settings);
                RegisterPool(prefab, pool);
            }
            GameObject obj = Instantiate(prefab, pool, position, rotation);
            if(!PoolsFromObjects.ContainsKey(obj)) PoolsFromObjects.Add(obj,pool);
            return obj;
        }

        public static GameObject Instantiate(GameObject prefab)
        {
            return Instantiate(prefab, Vector3.zero, Quaternion.identity);
        }

        public static void Destroy(GameObject obj)
        {
            if (PoolsFromObjects.TryGetValue(obj, out var pool))
            {
                RemoveFromPool(obj);
                Destroy(obj, pool);
            }
            else
            {
                Debug.LogWarning("Error on trying to destroy object to pool: trying this.InstantianteInPool before.");
                Object.Destroy(obj);
            }
            
        }
        
        public static bool RemoveFromPool(GameObject obj)
        {
            return PoolsFromObjects.Remove(obj);
        }
        #endregion

        #region Register and Unregister Pool
        // Register pool in dictionary with instance id pool data
        private static void RegisterPool(GameObject prefab, Pool pool)
        {
            PoolsFromPrefab.Add(prefab, pool);
        }

        // Unregister pool with instance id pool data
        private static void Unregister(GameObject prefab)
        {
            PoolsFromPrefab.Remove(prefab);
        }

        #endregion

    }
}