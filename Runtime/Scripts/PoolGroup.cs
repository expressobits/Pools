using System.Collections.Generic;
using UnityEngine;

namespace ExpressoBits.Pools
{
    [CreateAssetMenu(fileName = "Pool Group", menuName = "Expresso Bits/Pools/Pool Group")]
    public class PoolGroup : ScriptableObject
    {
        public List<Pool> Pools => pools;
        [SerializeField] private List<Pool> pools = new List<Pool>();

        public void AddPoolToList(Pool pool)
        {
            pools.Add(pool);
        }

        public void Clear()
        {
            foreach (var pool in pools)
            {
                pool.Clear();
            }
        }

        #region Instantiate
        public GameObject Instantiate(GameObject prefab)
        {
            foreach (Pool pool in pools)
            {
                if (pool.Prefab == prefab) return pool.Instantiate();
            }
            Debug.LogWarning("Pools no contains prefab in list!");
            return null;
        }

        public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            foreach (Pool pool in pools)
            {
                if (pool.Prefab == prefab) return pool.Instantiate(position,rotation);
            }
            Debug.LogWarning("Pools no contains prefab in list!");
            return null;
        }

        public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation, Transform parentTransform)
        {
            foreach (Pool pool in pools)
            {
                if (pool.Prefab == prefab) return pool.Instantiate(position,rotation,parentTransform);
            }
            Debug.LogWarning("Pools no contains prefab in list!");
            return null;
        }

        public GameObject Instantiate(GameObject prefab, Transform parentTransform)
        {
            foreach (Pool pool in pools)
            {
                if (pool.Prefab == prefab) return pool.Instantiate(parentTransform);
            }
            Debug.LogWarning("Pools no contains prefab in list!");
            return null;
        }

        public GameObject Instantiate(GameObject prefab, Transform parentTransform, bool instantiateInWorldSpace)
        {
            foreach (Pool pool in pools)
            {
                if (pool.Prefab == prefab) return pool.Instantiate(parentTransform,instantiateInWorldSpace);
            }
            Debug.LogWarning("Pools no contains prefab in list!");
            return null;
        }
        #endregion

        #region Instantiate Random
        public GameObject InstantiateARandom()
        {
            if (pools.Count == 0)
            {
                Debug.LogWarning("Pools empty list!");
                return null;
            }
            return pools[Random.Range(0, pools.Count)].Instantiate();
        }

        public GameObject InstantiateARandom(Transform parentTransform)
        {
            if (pools.Count == 0)
            {
                Debug.LogWarning("Pools empty list!");
                return null;
            }
            return pools[Random.Range(0, pools.Count)].Instantiate(parentTransform);
        }

        public GameObject InstantiateARandom(Vector3 position, Quaternion rotation, Transform parentTransform)
        {
            if (pools.Count == 0)
            {
                Debug.LogWarning("Pools empty list!");
                return null;
            }
            return pools[Random.Range(0, pools.Count)].Instantiate(position, rotation, parentTransform);
        }

        public GameObject InstantiateARandom(Transform parentTransform, bool instantiateInWorldSpace)
        {
            if (pools.Count == 0)
            {
                Debug.LogWarning("Pools empty list!");
                return null;
            }
            return pools[Random.Range(0, pools.Count)].Instantiate(parentTransform, instantiateInWorldSpace);
        }

        public GameObject InstantiateARandom(Vector3 position, Quaternion rotation)
        {
            if (pools.Count == 0)
            {
                Debug.LogWarning("Pools empty list!");
                return null;
            }
            return pools[Random.Range(0, pools.Count)].Instantiate(position, rotation);
        }
        #endregion

        public void Destroy(GameObject gameObject)
        {
            PoolManager.Destroy(gameObject);
        }
    }
}

