using System;
using UnityEngine;
using UnityEngine.Events;

namespace ExpressoBits.PoolSimply
{
    [AddComponentMenu("Pool/Pooler")]
    public class Pooler : MonoBehaviour, IPooler
    {
        [Header("Identification on pool")]
        public PoolData poolData;

        [Header("Events")]
        public UnityEvent OnEnableFromPool;
        public UnityEvent OnDisableFromPool;

        public void OnPoolerEnable()
        {
            OnEnableFromPool.Invoke();
        }

        public void OnPoolerDisable()
        {
            OnDisableFromPool.Invoke();
        }

        
        public void InstantiateInPool(){

            Pools.Instance().Instantiate(gameObject);

        }

        public void DestroyInPool(){

            Pools.Instance().Destroy(gameObject);
            
        }

    }
}
