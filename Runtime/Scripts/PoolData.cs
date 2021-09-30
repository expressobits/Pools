using UnityEngine;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace ExpressoBits.Pools
{
    [System.Serializable]
    public class PoolData
    {
        public Queue<GameObject> objects = new Queue<GameObject>();

        private uint IncreaseSize
        {
            get => m_IncreaseSize;
            set
            {
                if (value < 1) value = 1;
                m_IncreaseSize = value;
            }
        }
        [SerializeField] private uint m_IncreaseSize;
        
        public PoolData(PoolSettings settings)
        {
            IncreaseSize = settings.increaseSize;
            objects = new Queue<GameObject>();
        }

        #region EnqueueAndDequeue

        /**
         * Add to queue prefab and set object disabled
         **/
        public void Enqueue(GameObject obj)
        {
            if(objects == null) objects = new Queue<GameObject>();
            obj.SetActive(false);
            //OnPoolerDisable(obj);
            objects.Enqueue(obj);
        }

        /**
         * Get object from queue with prefab model, if no exist
         **/
        public GameObject Dequeue(GameObject prefab)
        {
            if(objects == null) objects = new Queue<GameObject>();
            if (objects.Count == 0)
            {
                InstantiateAmount(objects, prefab, (int)IncreaseSize);
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
            obj.transform.SetPositionAndRotation(position, rotation);
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