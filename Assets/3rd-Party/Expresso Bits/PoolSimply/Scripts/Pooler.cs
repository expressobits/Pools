using System;
using UnityEngine;
using UnityEngine.Events;

namespace ExpressoBits.PoolSimply
{
    [AddComponentMenu("PoolSimply/Pooler")]
    public class Pooler : MonoBehaviour, IPooler
    {
        [Header("Identification on pool")]
        public string id;

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

    }
}
