using UnityEngine;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace ExpressoBits.Pools
{
    public class Pool
    {
        public readonly Queue<GameObject> objects  = new Queue<GameObject>();
        private readonly PoolData m_PoolData;
        
        public Pool(PoolData poolData)
        {
            this.m_PoolData = poolData;
        }

        #region EnqueueAndDequeue
        /**
         * Add to queue prefab and set object disabled
         **/
        public void Enqueue(GameObject obj)
        {
            obj.SetActive(false);
            OnPoolerDisable(obj);
            objects.Enqueue(obj);
        }

        /**
         * Get object from queue with prefab model, if no exist
         **/
        public GameObject Dequeue(GameObject prefab)
        {
            // TODO Make this more efficiently
            if (objects.Count == 0){
                InstantiateAmount(objects,prefab,m_PoolData.InitialIncrease);
            }
            GameObject obj = objects.Dequeue();
            if(!obj){
                Object.Instantiate(prefab);
            }
            obj.SetActive(true);
            //OnPoolerEnable(obj);
            return obj;
        }
        
        public GameObject Dequeue(GameObject prefab,Vector3 position, Quaternion rotation)
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
        private void InstantiateAmount(Queue<GameObject> objs, GameObject prefab, int amount)
        {
            for (var i = 0; i < amount; i++)
            {
                var gameObject = Object.Instantiate(prefab);
                gameObject.SetActive(false);
                objs.Enqueue(gameObject);
            }
        }

        #region Utils
        public void Clear()
        {
            foreach (var obj in objects)
            {
                Object.Destroy(obj);
            }
            objects.Clear();
        }

        private void OnPoolerEnable(GameObject obj)
        {
            foreach (var ipooler in obj.GetComponents<IPooler>())
            {
                ipooler.OnPoolerEnable();
            }
        }

        private void OnPoolerDisable(GameObject obj)
        {
            foreach (var ipooler in obj.GetComponents<IPooler>())
            {
                ipooler.OnPoolerDisable();
            }
        }

        #endregion
    }
}

