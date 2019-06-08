using UnityEngine;
using System.Collections.Generic;


namespace ExpressoBits.PoolSimply
{

    /**
     * Class that manages the pool dictionary 
     * This class is static and is used by the MonoBehaviour Instantiate and Destroy method extenders
     * 
     **/
    [AddComponentMenu("PoolSimply/Pool")]
    public class Pool : MonoBehaviour
    {

        #region Data
        public Queue<GameObject> objects;
        #endregion
        

        private void Start() {
            objects = new Queue<GameObject>();
        }

        #region EnqueueAndDequeue
        /**
         * Add to queue prefab and set object disabled
         **/
        public void Enqueue(GameObject prefab)
        {
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
            PoolsData poolsData = pooler.poolsData;

            GameObject gameObject;

            if (objects.Count == 0){
                gameObject = Instantiate(prefab);
            }else{
                gameObject = objects.Dequeue();
            }
                
            #region TriggerComponent
            OnPoolerEnable(gameObject);
            gameObject.SetActive(true);
            #endregion

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



        public void Clear()
        {
            int count = objects.Count;
            for (int i = 0; i < count; i++)
            {
                GameObject obj = objects.Dequeue();
                Destroy(obj);
                Debug.Log("Destroy object");
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

