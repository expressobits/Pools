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
        private Queue<GameObject> objects;
        #endregion

        private void Start()
        {
            objects = new Queue<GameObject>();
        }

        #region EnqueueAndDequeue
        /**
         * Add to queue prefab and set object disabled
         **/
        public void Enqueue(GameObject prefab)
        {
            Pooler poolerComponent = prefab.GetComponent<Pooler>();

            #region TriggerComponent
            OnPoolerDisable(prefab);
            #endregion

            prefab.SetActive(false);
            objects.Enqueue(prefab);
        }

        /**
         * Get object from queue with prefab model, if no exist
         **/
        public GameObject Dequeue(GameObject prefab)
        {

            Pooler pooler = prefab.GetComponent<Pooler>();

            #region CheckIfDictionaryExist
            if (objects == null)
            {
                objects = new Queue<GameObject>();
                InstantiateAmount(objects, prefab, pooler.initialAmount);
            }
            #endregion

            if (objects.Count == 0)
            {
                if (!pooler.willGrow)
                {
                    Debug.LogWarning("Request from pool of object pools :" + name + " return null because overflow initial amount,you check 'Will Grow'?");
                    return null;
                }
                InstantiateAmount(objects, prefab, pooler.initialAmount);
            }
            GameObject gameObject = objects.Dequeue();

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
            int count = objects.Count;
            for (int i = 0; i < count; i++)
            {
                GameObject obj = objects.Dequeue();
                Destroy(obj);
                Debug.Log("Destruido obj");
            }
        }

        public void Clear()
        {
            int count = objects.Count;
            for (int i = 0; i < count; i++)
            {
                GameObject obj = objects.Dequeue();
                Destroy(obj);
                Debug.Log("Destruido obj");
            }
            objects.Clear();
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

