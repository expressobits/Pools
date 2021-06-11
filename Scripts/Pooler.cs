using System;
using UnityEngine;
using UnityEngine.Events;

namespace ExpressoBits.Pools
{
    [AddComponentMenu("Pool/Pooler")]
    public class Pooler : MonoBehaviour, IPooler
    {

        public PoolSettings poolSettings;

        [Header("Events")] 
        public UnityEvent onEnableFromPool;
        public UnityEvent onDisableFromPool;

        public void OnPoolerEnable()
        {
            onEnableFromPool.Invoke();
        }

        public void OnPoolerDisable()
        {
            onDisableFromPool.Invoke();
        }

        private void OnDestroy()
        {
            PoolManager.RemoveFromPool(gameObject);
        }
    }
}