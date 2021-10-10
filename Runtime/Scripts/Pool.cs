﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ExpressoBits.Pools
{
    // TODO transform to scriptable
    [CreateAssetMenu(menuName = "Expresso Bits/Pools/Pool", fileName = "Pool")]
    public class Pool : ScriptableObject, IPool
    {

        public PoolSettings Settings => settings;
        public GameObject Prefab => prefab;
        public Queue<GameObject> Objects => objects;

        [SerializeField] private PoolSettings settings = new PoolSettings(){ IncreaseSize = 1 };
        [SerializeField] private GameObject prefab;
        private Queue<GameObject> objects = new Queue<GameObject>();

        private void OnValidate()
        {
            settings.IncreaseSize = Math.Max(settings.IncreaseSize, 1);
        }

        #region Basic Methods
        public GameObject Instantiate()
        {
            return PoolManager.Instantiate(this);
        }

        public GameObject Instantiate(Vector3 position, Quaternion rotation)
        {
            return PoolManager.Instantiate(this, position, rotation);
        }

        public void Destroy(GameObject gameObject)
        {
            PoolManager.Destroy(gameObject, this);
        }
        #endregion

        #region EnqueueAndDequeue

        /**
         * Add to queue prefab and set object disabled
         **/
        public void Enqueue(GameObject obj)
        {
            if (objects == null) objects = new Queue<GameObject>();
            obj.SetActive(false);
            OnPoolerDisable(obj);
            objects.Enqueue(obj);
        }

        /**
         * Get object from queue with prefab model, if no exist
         **/
        public GameObject Dequeue()
        {
            if (objects == null) objects = new Queue<GameObject>();
            if (objects.Count == 0)
            {
                InstantiateAmount(objects, prefab, (int)Settings.IncreaseSize);
            }

            GameObject obj = objects.Dequeue();
            if (!obj)
            {
                obj = Object.Instantiate(prefab);
            }
            OnPoolerEnable(obj);
            obj.SetActive(true);
            return obj;
        }

        public void Setup(PoolSettings settings, GameObject prefab)
        {
            this.settings = settings;
            settings.IncreaseSize = Math.Max(settings.IncreaseSize, 1);
            this.prefab = prefab;
            objects = new Queue<GameObject>();
        }

        public GameObject Dequeue(Vector3 position, Quaternion rotation)
        {
            // NOTE This exists for cases that have calls in two instantaneous locations and only one has pooldata as a reference.
            PoolManager.RegisterPoolIfNotExists(this);
            //

            GameObject obj = Dequeue();
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