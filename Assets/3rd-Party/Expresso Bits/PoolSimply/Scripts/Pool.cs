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
        public PoolData poolData;
        public Queue<GameObject> objects  = new Queue<GameObject>();
        private Pooler pooler;
        public GameObject prefab;
        #endregion

        private void Awake() {
            Pools.RegisterPoolPrefab(prefab,this);
        }

        
        private void OnDisable() {
            Clear();
        }

        #region EnqueueAndDequeue
        /**
         * Add to queue prefab and set object disabled
         **/
        public void Enqueue(GameObject obj)
        {
            OnPoolerDisable(obj);
            //FIXME problem performance verification enable
            if(enabled){
                objects.Enqueue(obj);
            }else{
                Destroy(obj);
            }
            
        }

        /**
         * Get object from queue with prefab model, if no exist
         **/
        public GameObject Dequeue(GameObject prefab)
        {
            GameObject obj;
            //FIXME problem performance verification enable
            if(enabled){
                //TODO Make this more efficiely
                if (objects.Count == 0){
                    InstantiateAmount(objects,prefab,poolData.increaseAmount);
                }
                obj = objects.Dequeue();
                
            }else{
                obj = Instantiate(prefab);
            }
            OnPoolerEnable(obj);
            return obj;
            
        }
        
        public GameObject Dequeue(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            GameObject obj = Dequeue(prefab);
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            return obj;
        }
        #endregion

        /**
         * Instance amount gameobjects in queue first params
         **/
        private void InstantiateAmount(Queue<GameObject> gameObjects, GameObject prefab, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject gameObject = GameObject.Instantiate(prefab);
                gameObject.SetActive(false);
                gameObjects.Enqueue(gameObject);
            }
        }

        #region Utils
        public void Clear()
        {
            foreach (GameObject obj in objects)
            {
                Destroy((Object)obj);
            }
            objects.Clear();
        }
        

        public void OnPoolerEnable(GameObject obj)
        {
            obj.SetActive(true);
            foreach (IPooler pooler in obj.GetComponents<IPooler>())
            {
                pooler.OnPoolerEnable();
            }
        }

        public void OnPoolerDisable(GameObject obj)
        {
            obj.SetActive(false);
            foreach (IPooler pooler in obj.GetComponents<IPooler>())
            {
                pooler.OnPoolerDisable();
            }
        }
        #endregion

        #region Editor

        #endregion
    }
}

