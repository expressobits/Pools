using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ExpressoBits.PoolSimply
{
    public class Pools{

        public static Pools instance;

        public List<string> keys = new List<string>();
        public Dictionary<string,Pool> dictionary = new Dictionary<string,Pool>();

        [RuntimeInitializeOnLoadMethod]
        public static Pools Instance() {
            if(instance == null)
                instance = new Pools();

            return instance;
        }

        public void RegisterPoolPrefab(GameObject prefab, Pool pool){
            dictionary.Add(prefab.GetComponent<Pooler>().id,pool);
            keys.Add(prefab.GetComponent<Pooler>().id);
        }

        public GameObject Instantiate(GameObject prefab){
            Pool pool;
            dictionary.TryGetValue(prefab.GetComponent<Pooler>().id, out pool);
            return pool.Dequeue(prefab);
        }

        public GameObject Instantiate(GameObject prefab,Vector3 position,Quaternion rotation){
            Pool pool;
            dictionary.TryGetValue(prefab.GetComponent<Pooler>().id, out pool);
            return pool.Dequeue(prefab,position,rotation);
        }

        public void Destroy(GameObject obj){
            Pool pool;
            dictionary.TryGetValue(obj.GetComponent<Pooler>().id, out pool);
            pool.Enqueue(obj);
        }

        #if UNITY_EDITOR
        public List<string> GetKeyList(){
            return new List<string>(dictionary.Keys);
        }
        #endif

    }
}