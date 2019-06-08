using UnityEngine;
using System.Collections.Generic;


namespace ExpressoBits.PoolSimply
{

    /**
     * Class that manages the pool dictionary 
     * This class is static and is used by the MonoBehaviour Instantiate and Destroy method extenders
     * 
     **/
    [AddComponentMenu("PoolSimply/Pools")]
    public class Pools : MonoBehaviour
    {

        #region Data
        private List<string> keys;
        private Dictionary<string, Queue<GameObject>> poolDictionary;
        #endregion

        private void Start()
        {
            keys = new List<string>();
            poolDictionary = new Dictionary<string, Queue<GameObject>>();
        }

        #region EnqueueAndDequeue
        /**
         * Add to queue prefab disabled
         **/
        public void Enqueue(GameObject prefab)
        {
            Pooler poolerComponent = prefab.GetComponent<Pooler>();

            #region CheckDictionaryExists
            if (!poolDictionary.ContainsKey(poolerComponent.id))
            {
                keys.Add(poolerComponent.id);
                Debug.LogWarning("Pool enqueue with tag " + poolerComponent.id + "doesn't exist");
                poolDictionary[poolerComponent.id] = new Queue<GameObject>();
            }
            #endregion

            #region TriggerComponent
            OnPoolerDisable(prefab);
            #endregion

            prefab.SetActive(false);
            poolDictionary[poolerComponent.id].Enqueue(prefab);

        }

        /**
         * Get object from queue with prefab model, if no exist
         **/
        public GameObject Dequeue(GameObject prefab)
        {

            Pooler pooler = prefab.GetComponent<Pooler>();

            #region CheckIfDictionaryExist
            if (!poolDictionary.ContainsKey(pooler.id))
            {
                Debug.LogWarning("Pool with tag " + pooler.id + " doesn't exist");
                Queue<GameObject> gameObjects = new Queue<GameObject>();
                InstantiateAmount(gameObjects, prefab, pooler.initialAmount);
                poolDictionary.Add(pooler.id, gameObjects);
                keys.Add(pooler.id);
            }
            #endregion

            if (poolDictionary[pooler.id].Count == 0)
            {
                if (!pooler.willGrow)
                {
                    Debug.LogWarning("Request from pool of object pooler id:" + pooler.id + " return null because overflow initial amount,you check 'Will Grow'?");
                    return null;
                }
                InstantiateAmount(poolDictionary[pooler.id], prefab, pooler.initialAmount);
            }
            GameObject gameObject = poolDictionary[pooler.id].Dequeue();

            #region TriggerComponent
            OnPoolerEnable(gameObject);
            #endregion

            gameObject.SetActive(true);
            return gameObject;
        }
        #endregion

        #region Utils
        public GameObject Dequeue(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            GameObject gameObject = Dequeue(prefab);
            gameObject.transform.position = position;
            gameObject.transform.rotation = rotation;
            return gameObject;
        }

        /**
         * Instance amount gameobjects in queue first params
         **/
        private void InstantiateAmount(Queue<GameObject> gameObjects, GameObject prefab, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject gameObject = Instantiate(prefab);
                gameObject.SetActive(false);
                gameObjects.Enqueue(gameObject);
            }
        }

        public void DestroyAllObjects(string key)
        {
            int count = poolDictionary[key].Count;
            for (int i = 0; i < count; i++)
            {
                GameObject obj = poolDictionary[key].Dequeue();
                Destroy(obj);
                Debug.Log("Destruido obj");
            }
            poolDictionary.Remove(key);
        }

        public void Clear()
        {
            foreach (string key in keys)
            {
                DestroyAllObjects(key);
            }
            keys.Clear();
            poolDictionary.Clear();
        }

        public List<string> getKeys()
        {
            return keys;
        }

        public int CountOfKey(string key)
        {
            return poolDictionary[key].Count;
        }

        public void OnPoolerEnable(GameObject obj)
        {
            foreach (IPooler pooler in obj.GetComponents<IPooler>())
            {
                pooler.OnPoolerEnable();
            }
        }

        public void OnPoolerDisable(GameObject obj)
        {
            foreach (IPooler pooler in obj.GetComponents<IPooler>())
            {
                pooler.OnPoolerDisable();
            }
        }
        #endregion
    }
}

