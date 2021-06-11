using UnityEngine;

namespace ExpressoBits.Pools
{
    [System.Serializable]
    public struct PoolSettings
    {
        [Tooltip("Amount of a batch of gameObjects after there are no items in the pool")]
        public uint increaseSize;
    }
}