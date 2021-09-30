using UnityEngine;

namespace ExpressoBits.Pools
{
    [CreateAssetMenu(menuName = "ExpressoBits/Pool",fileName = "Pool")]
    public class Pool : ScriptableObject
    {
        public PoolData Data;
    }
}
