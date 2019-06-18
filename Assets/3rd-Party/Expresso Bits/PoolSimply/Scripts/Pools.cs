using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ExpressoBits.PoolSimply
{
    public class Pools {

        public static Pools instance;
        public List<int> ids = new List<int>();
        public Dictionary<int,Pool> pools = new Dictionary<int,Pool>();

        [RuntimeInitializeOnLoadMethod]
        public static Pools Instance() {
            if(instance == null)
                instance = new Pools();
            return instance;
        }

        #region Instantiate and Destory objects int pool
        public GameObject Instantiate(GameObject prefab){
            Pooler pooler = prefab.GetComponent<Pooler>();
            Pool pool = GetPoolFromPrefab(prefab,pools);
            return pool.Dequeue(prefab);
        }

        public GameObject Instantiate(GameObject prefab,Vector3 position,Quaternion rotation){
            Pooler pooler = prefab.GetComponent<Pooler>();
            Pool pool = GetPoolFromPrefab(prefab,pools);
            return pool.Dequeue(prefab,position,rotation).gameObject;
        }

        public void Destroy(GameObject obj){
            Pool pool = GetPoolFromPrefab(obj,pools);
            pool.Enqueue(obj);
        }
        #endregion
        
        #region Register and Unregister Pool
        // Register pool in dictionary with instance id pool data
        public void RegisterPool(PoolData poolData,Pool pool){
            int id = poolData.GetInstanceID();
            pools.Add(id,pool);
            ids.Add(id);
        }

        // Unregister pool with instance id pool data
        public void Unregister(PoolData poolData){
            int id = poolData.GetInstanceID();
            ids.Remove(id);
            pools.Remove(id);
        }
        #endregion

        // Search pool for prefab pooler pooldata instance id and return pool, 
        // if no found print error and return null
        // STUB  Try review code to optimize
        public static Pool GetPoolFromPrefab(GameObject prefab,Dictionary<int,Pool> pools){
            Pool pool;
            Pooler pooler = prefab.GetComponent<Pooler>();
            int id = pooler.poolData.GetInstanceID();
            bool exist = pools.TryGetValue(id, out pool);
            if(exist) return pool;
            Debug.LogWarning("No pool found for prefab "+prefab.name+", try to create or instantiate the Pool object in the scene.");
            return null;
        }

        #if UNITY_EDITOR
        public List<int> GetKeyList(){
            return new List<int>(pools.Keys);
        }
        #endif

    }
}