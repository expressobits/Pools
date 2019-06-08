using System;
using UnityEngine;
using UnityEngine.Events;

namespace ExpressoBits.PoolSimply
{
    [AddComponentMenu("PoolSimply/Pooler")]
    public class Pooler : MonoBehaviour, IPooler
    {
        public string id = "test";
        public int initialAmount = 20;
        public bool willGrow = true;
        public int increaseAmount = 5;

        public Pools pools;

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
