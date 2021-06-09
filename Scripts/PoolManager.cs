using UnityEngine;
using System.Collections.Generic;

namespace ExpressoBits.Pools
{
    public class PoolManager
    {
        public static PoolManager instance;
        public readonly List<int> ids = new List<int>();
        public readonly Dictionary<int, Pool> pools = new Dictionary<int, Pool>();

        [RuntimeInitializeOnLoadMethod]
        public static PoolManager Instance()
        {
            if (instance == null)
                instance = new PoolManager();
            return instance;
        }

        #region Instantiate and Destroy objects in pool

        public GameObject Instantiate(GameObject prefab, Pool pool, Vector3 position, Quaternion rotation)
        {
            return pool.Dequeue(prefab, position, rotation);
        }

        public GameObject Instantiate(GameObject prefab, PoolData poolData, Vector3 position, Quaternion rotation)
        {
            Pool pool = GetPoolFromPoolData(poolData);
            return Instantiate(prefab, pool, position, rotation);
        }

        public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            if (prefab.TryGetComponent(out Pooler pooler))
            {
                return Instantiate(prefab, pooler.poolData, position, rotation);
            }

            Debug.Assert(pooler, "GameObject without Pooler component!");
            return Object.Instantiate(prefab);
        }

        public GameObject Instantiate(GameObject prefab)
        {
            return Instantiate(prefab, Vector3.zero, Quaternion.identity);
        }

        public void Destroy(GameObject obj)
        {
            if (obj.TryGetComponent(out Pooler pooler))
            {
                Destroy(obj, pooler.poolData);
            }

            Debug.Assert(pooler, "GameObject without Pooler component!");
            Object.Destroy(obj);
        }

        public void Destroy(GameObject obj, PoolData poolData)
        {
            // TODO problem with obj != prefab
            Pool pool = GetPoolFromPoolData(poolData);
            Destroy(obj, pool);
        }

        public void Destroy(GameObject obj, Pool pool)
        {
            pool.Enqueue(obj);
        }

        #endregion

        #region Register and Unregister Pool

        // Register pool in dictionary with instance id pool data
        private void RegisterPool(PoolData poolData, Pool pool)
        {
            var id = poolData.GetInstanceID();
            pools.Add(id, pool);
            ids.Add(id);
        }

        // Unregister pool with instance id pool data
        private void Unregister(PoolData poolData)
        {
            int id = poolData.GetInstanceID();
            ids.Remove(id);
            pools.Remove(id);
        }

        #endregion

        // Search pool for prefab pooler pooldata instance id and return pool, 
        // if no found print error and return null
        // STUB Try review code to optimize
        private Pool GetPoolFromPoolData(PoolData poolData)
        {
            var id = poolData.GetInstanceID();
            var exist = pools.TryGetValue(id, out var pool);
            if (exist) return pool;
            pool = new Pool(poolData.InitialIncrease);
            instance.RegisterPool(poolData, pool);
            return pool;
        }

#if UNITY_EDITOR
        public List<int> GetKeyList()
        {
            //return new List<int>(pools.Keys);
            return null;
        }
#endif
    }
}