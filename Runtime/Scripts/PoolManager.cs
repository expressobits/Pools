using UnityEngine;
using System.Collections.Generic;

namespace ExpressoBits.Pools
{
    public class PoolManager
    {
        private static readonly Dictionary<GameObject, IPool> PoolsFromPrefab = new Dictionary<GameObject, IPool>();
        private static readonly Dictionary<GameObject, IPool> PoolsFromObjects = new Dictionary<GameObject, IPool>();
        private const uint defaultIncreaseSize = 5;

        #region Basic Functions
        public static GameObject Instantiate(IPool pool, Vector3 position, Quaternion rotation)
        {
            return pool.Instantiate(position, rotation);
        }
        public static void Destroy(GameObject obj, IPool pool)
        {
            pool.Destroy(obj);
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
                    settings = new PoolSettings{ IncreaseSize = defaultIncreaseSize };
                }
                pool = ScriptableObject.CreateInstance<Pool>();
                pool.Setup(settings,prefab);
                RegisterPool(pool);
            }
            GameObject obj = Instantiate(pool, position, rotation);
            if(!PoolsFromObjects.ContainsKey(obj)) PoolsFromObjects.Add(obj,pool);
            return obj;
        }

        public static GameObject Instantiate(Pool data)
        {
            return Instantiate(data, Vector3.zero, Quaternion.identity);
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
        private static void RegisterPool(IPool pool)
        {
            PoolsFromPrefab.Add(pool.Prefab, pool);
        }

        // Unregister pool with instance id pool data
        private static void Unregister(GameObject prefab)
        {
            PoolsFromPrefab.Remove(prefab);
        }

        #endregion

        public static void RegisterPoolIfNotExists(Pool pool)
        {
            if (!PoolsFromPrefab.ContainsKey(pool.Prefab)) RegisterPool(pool);
        }

    }
}