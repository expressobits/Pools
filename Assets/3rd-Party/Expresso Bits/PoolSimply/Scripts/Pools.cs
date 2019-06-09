using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ExpressoBits.PoolSimply
{
    public class Pools{

        public static Pools instance;

        //private List<GameObject> prefabs;
        private static Dictionary<string,Pool> dictionary = new Dictionary<string,Pool>();

        [RuntimeInitializeOnLoadMethod]
        private void Init() {
            instance = this;
        }

        public static void RegisterPoolPrefab(GameObject prefab, Pool pool){
            dictionary.Add(prefab.GetComponent<Pooler>().id,pool);
        }

        public static GameObject Instantiate(GameObject prefab){
            Pool pool;
            dictionary.TryGetValue(prefab.GetComponent<Pooler>().id, out pool);
            return pool.Dequeue(prefab);
        }

        public static GameObject Instantiate(GameObject prefab,Vector3 position,Quaternion rotation){
            Pool pool;
            dictionary.TryGetValue(prefab.GetComponent<Pooler>().id, out pool);
            return pool.Dequeue(prefab,position,rotation);
        }

        public static void Destroy(GameObject obj){
            Pool pool;
            dictionary.TryGetValue(obj.GetComponent<Pooler>().id, out pool);
            pool.Enqueue(obj);
        }

    }
}