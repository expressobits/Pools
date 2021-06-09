using UnityEngine;
using UnityEngine.Events;

namespace ExpressoBits.Pools
{
    [AddComponentMenu("Pool/Pooler")]
    public class Pooler : MonoBehaviour, IPooler
    {
        [Header("Identification on pool")] public PoolData poolData;

        [Header("Events")] public UnityEvent onEnableFromPool;
        public UnityEvent onDisableFromPool;

        public void OnPoolerEnable()
        {
            onEnableFromPool.Invoke();
        }

        public void OnPoolerDisable()
        {
            onDisableFromPool.Invoke();
        }
    }
}