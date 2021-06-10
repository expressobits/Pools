using UnityEngine;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace ExpressoBits.Pools
{
    public class Pool
    {
        public readonly Queue<GameObject> objects = new Queue<GameObject>();

        private int m_IncreaseSize;
        
        public Pool(int increaseSize)
        {
            if (increaseSize < 1) increaseSize = 1;
            this.m_IncreaseSize = increaseSize;
        }

        #region EnqueueAndDequeue

        /**
         * Add to queue prefab and set object disabled
         **/
        public void Enqueue(GameObject obj)
        {
            obj.SetActive(false);
            //OnPoolerDisable(obj);
            objects.Enqueue(obj);
        }

        /**
         * Get object from queue with prefab model, if no exist
         **/
        public GameObject Dequeue(GameObject prefab)
        {
            if (objects.Count == 0)
            {
                InstantiateAmount(objects, prefab, m_IncreaseSize);
            }

            GameObject obj = objects.Dequeue();
            if (!obj)
            {
                obj = Object.Instantiate(prefab);
            }
            obj.SetActive(true);
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
         * Instance amount GameObjects in queue first params
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