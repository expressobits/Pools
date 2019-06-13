using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ExpressoBits.PoolSimply
{
    //FIXME Problem on remove list on disable scene
    public class Pools{

        public static Pools instance;
        public List<int> ids = new List<int>();
        public Dictionary<int,PoolData> poolDatas = new Dictionary<int,PoolData>();
        public Dictionary<int,Pool> pools = new Dictionary<int,Pool>();

        [RuntimeInitializeOnLoadMethod]
        public static Pools Instance() {
            if(instance == null)
                instance = new Pools();
            return instance;
        }

        public GameObject Instantiate(GameObject prefab){
            Pooler pooler = prefab.GetComponent<Pooler>();
            
            Pool pool = GetPoolFromPrefab(prefab);
            return pool.Dequeue(prefab);
        }

        public GameObject Instantiate(GameObject prefab,Vector3 position,Quaternion rotation){
            Pool pool = GetPoolFromPrefab(prefab);
            return pool.Dequeue(position,rotation).gameObject;
        }

        public void Destroy(GameObject obj){
            Pool pool = GetPoolFromPrefab(obj);
            pool.Enqueue(obj);
        }

        public Pool GetPoolFromPrefab(GameObject prefab){
            Pool pool;
            Pooler pooler = prefab.GetComponent<Pooler>();
            int id = pooler.poolData.GetInstanceID();
            bool exist = pools.TryGetValue(id, out pool);
            if(!exist){
                pool = new Pool(prefab,pooler.poolData);
                pools.Add(id,pool);
                poolDatas.Add(id,pooler.poolData);
                ids.Add(id);
            }
            return pool;
        }

        #if UNITY_EDITOR
        public List<int> GetKeyList(){
            return new List<int>(pools.Keys);
        }
        #endif

    }
}